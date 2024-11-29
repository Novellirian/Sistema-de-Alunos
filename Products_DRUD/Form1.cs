using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Products_DRUD
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            LoadDataGrid();
        }

        // Método para carregar os dados na DataGridView
        private void LoadDataGrid()
        {
            var connection = DatabaseConnection.Instance.GetConnection();

            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                string query = "SELECT * FROM alunos";
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGrid.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar os dados: " + ex.Message);
            }
        }

        // Criar um novo aluno
        private void btnCreate_Click(object sender, EventArgs e)
        {
            var connection = DatabaseConnection.Instance.GetConnection();

            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                string query = "INSERT INTO alunos (nome, idade, curso, matricula) VALUES (@nome, @idade, @curso, @matricula)";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@nome", txtName.Text);
                command.Parameters.AddWithValue("@idade", txtPrice.Text);
                command.Parameters.AddWithValue("@curso", txtDescription.Text);
                command.Parameters.AddWithValue("@matricula", txtMatricula.Text);

                command.ExecuteNonQuery();
                MessageBox.Show("Aluno cadastrado com sucesso!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao cadastrar aluno: " + ex.Message);
            }

            LoadDataGrid();
        }

        // Ler dados de um aluno usando a matrícula
        private void btnRead_Click(object sender, EventArgs e)
        {
            var connection = DatabaseConnection.Instance.GetConnection();

            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                string query = "SELECT * FROM alunos WHERE matricula = @matricula";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@matricula", txtMatricula.Text);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        txtName.Text = reader["nome"].ToString();
                        txtPrice.Text = reader["idade"].ToString();
                        txtDescription.Text = reader["curso"].ToString();
                        txtMatricula.Text = reader["matricula"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("Aluno não encontrado!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao buscar aluno: " + ex.Message);
            }
        }

        // Atualizar dados de um aluno
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGrid.SelectedRows.Count > 0)
            {
                int alunoId = Convert.ToInt32(dataGrid.SelectedRows[0].Cells["id"].Value);
                var connection = DatabaseConnection.Instance.GetConnection();

                try
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }

                    string query = "UPDATE alunos SET nome = @nome, idade = @idade, curso = @curso, matricula = @matricula WHERE id = @id";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@nome", txtName.Text);
                    command.Parameters.AddWithValue("@idade", txtPrice.Text);
                    command.Parameters.AddWithValue("@curso", txtDescription.Text);
                    command.Parameters.AddWithValue("@matricula", txtMatricula.Text);
                    command.Parameters.AddWithValue("@id", alunoId);

                    command.ExecuteNonQuery();
                    MessageBox.Show("Dados do aluno atualizados com sucesso!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao atualizar aluno: " + ex.Message);
                }

                LoadDataGrid();
            }
            else
            {
                MessageBox.Show("Selecione um aluno na tabela para atualizar.");
            }
        }

        // Deletar um aluno
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGrid.SelectedRows.Count > 0)
            {
                int alunoId = Convert.ToInt32(dataGrid.SelectedRows[0].Cells["id"].Value);
                var connection = DatabaseConnection.Instance.GetConnection();

                try
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }

                    string query = "DELETE FROM alunos WHERE id = @id";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@id", alunoId);

                    command.ExecuteNonQuery();
                    MessageBox.Show("Aluno removido com sucesso!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao remover aluno: " + ex.Message);
                }

                LoadDataGrid();
            }
            else
            {
                MessageBox.Show("Selecione um aluno na tabela para deletar.");
            }
        }

        // Método para preencher os campos do formulário com os dados selecionados no DataGrid
        private void grid_SelectionChange(object sender, EventArgs e)
        {
            if (dataGrid.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGrid.SelectedRows[0];
                txtName.Text = selectedRow.Cells["nome"].Value.ToString();
                txtPrice.Text = selectedRow.Cells["idade"].Value.ToString();
                txtDescription.Text = selectedRow.Cells["curso"].Value.ToString();
                txtMatricula.Text = selectedRow.Cells["matricula"].Value.ToString();
            }
        }

        // Carregar a tabela (botão específico)
        private void btn_LoadTable(object sender, EventArgs e)
        {
            LoadDataGrid();
        }

        // Métodos vazios para eventos de clique nos labels
        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void label3_Click(object sender, EventArgs e)
        {
        }

        private void label4_Click(object sender, EventArgs e)
        {
        }
    }
}
