using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Newtonsoft.Json.Linq;

namespace O_ALMOC.AR
{
    public partial class MainPage : ContentPage
    {
        Receita receita = new Receita();
        public MainPage()
        {
            InitializeComponent();
            BindingContext = receita;
        }

        private HttpClient _client = new HttpClient();
        void clicked(object sender, EventArgs args)
        {
            Random aleatorio = new Random();
            int numeroAleatorio = aleatorio.Next(1, 20);

            Receitas(numeroAleatorio);
        }

        async Task Receitas(int id)
        {
            string apiUrl = $"https://gold-anemone-wig.cyclic.app/receitas/{id}";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResult = await response.Content.ReadAsStringAsync();
                        JObject jsonObject = JObject.Parse(jsonResult);

                        Receita receita = new Receita();
                        receita.nome = jsonObject["receita"].ToString();
                        receita.foto = jsonObject["link_imagem"].ToString();
                        receita.ingredientes = jsonObject["ingredientes"].ToString();
                        receita.modo_de_preparo = jsonObject["modo_preparo"].ToString();

                        BindingContext = receita;
                    }
                    else
                    {
                        Console.WriteLine($"Erro na solicitação: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro: {ex.Message}");
                }
            }
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string jsonResult = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(jsonResult);
                }
                else
                {
                    Console.WriteLine($"Erro na solicitação: {response.StatusCode}");
                }
            }
        }
    }
}

