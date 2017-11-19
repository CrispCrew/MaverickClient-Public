using Discord;
using Discord.Commands;
using DiscordBot.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Modules
{
    public class Cheats : ModuleBase<SocketCommandContext>
    {
        Client client = Program.client;

        [Command("Cheats")]
        [Alias("c")]
        public async Task GetProducts()
        {
            string token;

            List<Product> products = new List<Product>();

            bool server = client.ServerCheck();
            bool logins = client.Login(out token);

            //Products
            string owned = client.Products(token).Replace("Packages=", "");
            if (owned != "" && owned != "Authentication Token was not found")
            {
                if (!owned.Contains("|"))
                {
                    string[] product_details = owned.Split(':');

                    int Id = Convert.ToInt32(product_details[0]); //UID
                    string Name = product_details[1]; //Product Name
                    string File = product_details[2]; //Product Media
                    string ProcessName = product_details[3]; //Product Media
                    int Status = Convert.ToInt32(product_details[4]); //Product Status
                    int Version = Convert.ToInt32(product_details[5]);
                    int Free = Convert.ToInt32(product_details[6]);
                    long AutoLaunch = Convert.ToInt64(product_details[7]);

                    products.Add(new Product(Id, Name, File, ProcessName, Status, Version, Free, AutoLaunch));
                }
                else
                {
                    foreach (string product in owned.Split('|'))
                    {
                        string[] product_details = product.Split(':');

                        int Id = Convert.ToInt32(product_details[0]); //UID
                        string Name = product_details[1]; //Product Name
                        string File = product_details[2]; //Product Media
                        string ProcessName = product_details[3]; //Product Media
                        int Status = Convert.ToInt32(product_details[4]); //Product Status
                        int Version = Convert.ToInt32(product_details[5]);
                        int Free = Convert.ToInt32(product_details[6]);
                        long AutoLaunch = Convert.ToInt64(product_details[7]);

                        products.Add(new Product(Id, Name, File, ProcessName, Status, Version, Free, AutoLaunch));
                    }
                }
            }
            else
            {
                await Context.Message.Channel.SendMessageAsync("There are no current products in the database!");

                return;
            }

            EmbedBuilder eb = new EmbedBuilder();
            eb.Color = products.Any(product => product.status == -1) ? new Color(255, 0, 0) : (products.Any(product => product.status == 0) ? new Color(255, 128, 0) : new Color(0, 255, 0));
            eb.Title = "Cheats List";

            foreach (Product product in products)
            {
                eb.Description += "ID: " + product.id + " - Name: " + product.name + " - v" + product.version + " - Status: " + product.StatusIDToString() + " - " + "Free: " + ((product.free == 1) ? "Yes" : "No") + "\n";
            }

            await Context.Channel.SendMessageAsync("", false, eb);
        }
    }
}
