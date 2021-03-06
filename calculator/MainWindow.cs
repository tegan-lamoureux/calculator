﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class MainWindow : Form
    {
        private double running_value_previous = 0.0;  // Keep the previous value for use in consecutive equals.
        private double running_value = 0.0;           // Keep the running total, used for results and operations.

        private bool operation_performed = false;     // Know whether an operations key was just pressed.

        private char last_operation = ' ';            // Keep track of the last operation, for use in equals.
        private char last_operation_previous = ' ';   // Keep track of the previous last operation, also for use in equals.

        public MainWindow()
        {
            InitializeComponent();
        }

        private void handle_numbers_click(char value)
        {
            // Used to check whether we just performed an operation. If not, keep parsing and building input.
            if (!this.operation_performed && this.textBox_result.Text != "0.0")
            {
                // Check if the last op was an equals, in which case we start a new calculation (and reset).
                if (this.last_operation == '=')
                {
                    // reset everything
                    this.textBox_result.Text = value.ToString();
                    this.operation_performed = false;
                    this.running_value = 0.0;
                    this.last_operation = ' ';
                }
                else // else we're in the middle of input, keep concating the string.
                    this.textBox_result.Text += value;
            }
            else // Else we just performed a calc, clear the field and get ready for new input.
            {
                this.textBox_result.Text = value.ToString();
                this.operation_performed = false;
            }
        }

        private void button_decimal_Click(object sender, EventArgs e)
        {
            // FIXME: this is a bit buggy on fringe cases, fix this.
            if (this.textBox_result.Text.Contains(".") == false)
            {
                this.textBox_result.Text += ".";
            }
        }

        private void button_zero_Click(object sender, EventArgs e)
        {
            this.handle_numbers_click('0');
        }

        private void button_one_Click(object sender, EventArgs e)
        {
            this.handle_numbers_click('1');
        }

        private void button_two_Click(object sender, EventArgs e)
        {
            this.handle_numbers_click('2');
        }

        private void button_three_Click(object sender, EventArgs e)
        {
            this.handle_numbers_click('3');
        }

        private void button_four_Click(object sender, EventArgs e)
        {
            this.handle_numbers_click('4');
        }

        private void button_five_Click(object sender, EventArgs e)
        {
            this.handle_numbers_click('5');
        }

        private void button_six_Click(object sender, EventArgs e)
        {
            this.handle_numbers_click('6');
        }

        private void button_seven_Click(object sender, EventArgs e)
        {
            this.handle_numbers_click('7');
        }

        private void button_eight_Click(object sender, EventArgs e)
        {
            this.handle_numbers_click('8');
        }

        private void button_nine_Click(object sender, EventArgs e)
        {
            this.handle_numbers_click('9');
        }

        private void button_equals_Click(object sender, EventArgs e)
        {
            // Switch on the last operation performed, to finish the calculation.
            switch (this.last_operation)
            {
                case '=': // In the case that = was pressed twice, repeat the previous calculation.
                    switch (this.last_operation_previous)
                    {
                        case '+':
                            this.running_value += this.running_value_previous;
                            break;

                        case '-':
                            this.running_value -= this.running_value_previous;
                            break;

                        case '*':
                            this.running_value *= this.running_value_previous;
                            break;

                        case '/':
                            this.running_value /= this.running_value_previous;
                            break;
                    }
                break;

                case '+':
                    this.last_operation_previous = '+';
                    this.running_value_previous = Convert.ToDouble(this.textBox_result.Text);
                    this.running_value += Convert.ToDouble(this.textBox_result.Text);
                    break;

                case '-':
                    this.last_operation_previous = '-';
                    this.running_value_previous = Convert.ToDouble(this.textBox_result.Text);
                    this.running_value -= Convert.ToDouble(this.textBox_result.Text);
                    break;

                case '*':
                    this.last_operation_previous = '*';
                    this.running_value_previous = Convert.ToDouble(this.textBox_result.Text);
                    this.running_value *= Convert.ToDouble(this.textBox_result.Text);
                    break;

                case '/':
                    this.last_operation_previous = '/';
                    this.running_value_previous = Convert.ToDouble(this.textBox_result.Text);
                    this.running_value /= Convert.ToDouble(this.textBox_result.Text);
                    break;

                default:
                    this.running_value = Convert.ToDouble(this.textBox_result.Text);
                    break;
            }

            // Update our answer field.
            this.textBox_result.Text = this.running_value.ToString();

            // Keep track of last op (because we got here with an '=' click.
            this.last_operation = '=';
        }

        private void button_add_Click(object sender, EventArgs e)
        {
            // Don't operate on empty values.
            if (this.running_value != 0.0)
            {
                // Here to stop the automatic subtract behavior when - pressed after an equals or on empty.
                if (this.last_operation != '=')
                    this.running_value += Convert.ToDouble(textBox_result.Text);

                this.textBox_result.Text = this.running_value.ToString();
            }
            else
                this.running_value = Convert.ToDouble(textBox_result.Text);

            operation_performed = true;
            this.last_operation = '+';
        }

        private void button_divide_Click(object sender, EventArgs e)
        {
            if (this.running_value != 0.0)
            {
                // Here to stop the automatic subtract behavior when - pressed after an equals or on empty.
                if (this.last_operation != '=')
                    this.running_value /= Convert.ToDouble(textBox_result.Text);

                this.textBox_result.Text = this.running_value.ToString();
            }
            else
                this.running_value = Convert.ToDouble(textBox_result.Text);

            operation_performed = true;
            this.last_operation = '/';
        }

        private void button_all_clear_Click(object sender, EventArgs e)
        {
            this.running_value = 0.0;
            this.running_value_previous = 0.0;
            this.last_operation = ' ';
            this.last_operation_previous = ' ';
            this.textBox_result.Text = "0.0";
            this.operation_performed = false;
        }

        private void button_clear_Click(object sender, EventArgs e)
        {
            // Clear the text field (used as numerical input).
            this.textBox_result.Text = "0.0";

            // And all clear if we were on the result of a previous operation
            if (this.last_operation == '=')
            {
                this.running_value = 0.0;
                this.running_value_previous = 0.0;
                this.last_operation = ' ';
                this.last_operation_previous = ' ';
                this.operation_performed = false;
            }
        }

        private void button_subtract_Click(object sender, EventArgs e)
        {
            if (this.running_value != 0.0)
            {
                // Here to stop the automatic subtract behavior when - pressed after an equals or on empty.
                if (this.last_operation != '=')
                    this.running_value -= Convert.ToDouble(textBox_result.Text);

                this.textBox_result.Text = this.running_value.ToString();
            }
            else
                this.running_value = Convert.ToDouble(textBox_result.Text);

            operation_performed = true;
            this.last_operation = '-';
        }

        private void button_multiply_Click(object sender, EventArgs e)
        {
            if (this.running_value != 0.0)
            {
                // Here to stop the automatic subtract behavior when - pressed after an equals or on empty.
                if (this.last_operation != '=')
                    this.running_value *= Convert.ToDouble(textBox_result.Text);

                this.textBox_result.Text = this.running_value.ToString();
            }
            else
                this.running_value = Convert.ToDouble(textBox_result.Text);

            operation_performed = true;
            this.last_operation = '*';
        }
    }
}
