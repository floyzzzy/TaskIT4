using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Reflection;

namespace GeometryLib
{
    public partial class MainForm : Form
    {
        private Assembly loadedAssembly;
        private Type selectedType;
        private Dictionary<string, TextBox> parameterBoxes;

        public MainForm()
        {
            InitializeComponent();
            parameterBoxes = new Dictionary<string, TextBox>();
            LoadTypes(); // Сразу загружаем типы, так как они теперь в той же сборке
        }

        private void LoadTypes()
        {
            cmbTypes.Items.Clear();
            var types = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.IsSubclassOf(typeof(GeometricFigure)) && !t.IsAbstract)
                .ToList();

            foreach (var type in types)
            {
                cmbTypes.Items.Add(type.Name);
            }

            if (cmbTypes.Items.Count > 0)
                cmbTypes.SelectedIndex = 0;
        }

        private void cmbTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTypes.SelectedItem == null) return;

            selectedType = Assembly.GetExecutingAssembly().GetTypes()
                .FirstOrDefault(t => t.Name == cmbTypes.SelectedItem.ToString());

            if (selectedType != null)
                LoadMethods();
        }

        private void LoadMethods()
        {
            cmbMethods.Items.Clear();
            var methods = selectedType.GetMethods()
                .Where(m => m.DeclaringType == selectedType || m.DeclaringType == typeof(GeometricFigure))
                .Where(m => !m.IsSpecialName) // Исключаем специальные методы (get/set свойств)
                .ToList();

            foreach (var method in methods)
            {
                cmbMethods.Items.Add(method.Name);
            }

            if (cmbMethods.Items.Count > 0)
                cmbMethods.SelectedIndex = 0;
        }

        private void cmbMethods_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMethods.SelectedItem == null) return;

            CreateParameterInputs();
        }

        private void CreateParameterInputs()
        {
            pnlParameters.Controls.Clear();
            parameterBoxes.Clear();

            var constructor = selectedType.GetConstructors().First();
            var parameters = constructor.GetParameters();

            int yOffset = 10;
            foreach (var param in parameters)
            {
                var label = new Label
                {
                    Text = $"{param.Name}:",
                    Location = new System.Drawing.Point(10, yOffset),
                    AutoSize = true
                };

                var textBox = new TextBox
                {
                    Location = new System.Drawing.Point(150, yOffset),
                    Width = 100
                };

                parameterBoxes.Add(param.Name, textBox);
                pnlParameters.Controls.Add(label);
                pnlParameters.Controls.Add(textBox);

                yOffset += 30;
            }
        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            try
            {
                var constructor = selectedType.GetConstructors().First();
                var parameters = constructor.GetParameters();
                var paramValues = new object[parameters.Length];

                for (int i = 0; i < parameters.Length; i++)
                {
                    if (!parameterBoxes.TryGetValue(parameters[i].Name, out TextBox textBox))
                        throw new Exception($"Не найдено поле для параметра {parameters[i].Name}");

                    if (!double.TryParse(textBox.Text, out double value))
                        throw new Exception($"Неверный формат числа для параметра {parameters[i].Name}");

                    paramValues[i] = value;
                }

                var instance = constructor.Invoke(paramValues);
                var method = selectedType.GetMethod(cmbMethods.SelectedItem.ToString());
                var result = method.Invoke(instance, null);

                if (result is double[] array)
                {
                    txtResult.Text = string.Join(", ", array);
                }
                else
                {
                    txtResult.Text = result.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка выполнения: {ex.Message}");
            }
        }
    }
} 