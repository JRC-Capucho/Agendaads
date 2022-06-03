namespace AgendaADS; 

    public partial class Form2 : Form
    {
        private Db bancoDados = new Db();
        public Form2()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btVoltar_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f1 = new Form1();
            f1.ShowDialog();
            this.Close();
        }

        private void btBuscar_Click(object sender, EventArgs e)
        {
            String[] aux = bancoDados.buscarAula(tbDisciplina.Text);
            tbProfessor.Text = aux[0];
            tbDisciplina.Text = aux[1];
            tbInicioAula.Text = aux[2];
            tbTerminoAula.Text = aux[3];
            tbLocalAula.Text = aux[4];
            tbComoChegar.Text = aux[5];
            tbDiaSemana.Text = aux[6];
        }

        private void btExcluir_Click(object sender, EventArgs e)
        {
            bancoDados.excluirAula(tbDisciplina.Text);
        }

        private void btAlterar_Click(object sender, EventArgs e)
        {
            bancoDados.atualizarAula(tbProfessor.Text,tbDisciplina.Text,tbInicioAula.Text,tbTerminoAula.Text, tbLocalAula.Text,tbComoChegar.Text,tbDiaSemana.Text);
        }

        private void btRegistrar_Click(object sender, EventArgs e)
        {
            bancoDados.adicionarAula(tbProfessor.Text,tbDisciplina.Text,tbInicioAula.Text,tbTerminoAula.Text, tbLocalAula.Text,tbComoChegar.Text,tbDiaSemana.Text);
        }

    }