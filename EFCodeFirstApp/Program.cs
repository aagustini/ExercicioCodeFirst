﻿using PL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCodeFirstApp
{
    class Program
    {
        static void Main(string[] args)
        {
            #region - CRUD

            //// crud - adiciona na um novo filme à coleção
            ////        remove o primeiro filme do banco
            ////        atualiza os dados de um filme
            //using (var contexto = new MovieContext())
            //{
            //    var listaFilmes = contexto.Movies.ToList();

            //    // insert
            //    contexto.Movies.Add(new Movie()
            //    {
            //        Title = "Logan2",
            //        Director = "James Mangold",
            //        Rating = 8.5,
            //        ReleaseDate = new DateTime(2017, 03, 24),
            //        GenreID = 1
            //    });

            //    // edit
            //    Movie batman = listaFilmes.Where(f => f.Title == "The Dark Knight").FirstOrDefault<Movie>();
            //    if (batman != null)
            //        batman.Title = "Batman - " + batman.Title;

            //    // delete
            //    contexto.Movies.Remove(listaFilmes.ElementAt<Movie>(0));

            //    // persistir
            //    contexto.Database.Log = Console.Write;
            //    contexto.SaveChanges();
            //}


            //// lista todos os generos
            //using (var contexto = new MovieContext())
            //{
            //    //contexto.Database.Log = Console.Write;

            //    Console.WriteLine("Todos os generos");
            //    foreach (Genre genero in contexto.Genres)
            //    {
            //        Console.WriteLine("{0} \t {1}", genero.GenreID, genero.Name);

            //    }
            //}

            //Console.WriteLine("\n");
            //// lista todos os filmes do genero "Action"
            //using (var contexto = new MovieContext())
            //{
            //    contexto.Database.Log = Console.Write;
            //    Genre genero = contexto.Genres.Find(1);
            //    if (genero != null)
            //    {
            //        Console.WriteLine("\nFilmes do genero: " + genero.Name);
            //        foreach (Movie filme in genero.Movies)
            //        {
            //            Console.WriteLine("\t{0}", filme.Title);

            //        }
            //    }
            //}

            //// gera uma exceção pois o contexto não está disponível
            //Console.WriteLine("\nDesconectado...\n");
            //MovieContext cntx = new MovieContext();
            //cntx.Database.Log = Console.Write;
            //Genre action = cntx.Genres.Find(1);
            //cntx.Dispose();
            //if (action != null)
            //{
            //    Console.WriteLine("\nFilmes do genero: " + action.Name);

            //    foreach (Movie filme in action.Movies)
            //    {
            //        Console.WriteLine("\t{0}", filme.Title);
            //    }
            //}


            //// Desconectato
            //MovieContext cntx = new MovieContext();
            //cntx.Database.Log = Console.Write;
            //List<Genre> generos = cntx.Genres.ToList<Genre>();
            //cntx.Dispose();
            //foreach (Genre genero in generos)
            //{
            //    Console.WriteLine("{0} \t {1}", genero.GenreID, genero.Name);
            //}

            //cntx = new MovieContext();
            ////cntx.Database.Log = Console.Write;
            ////var action= cntx.Genres.Find(1);
            //var action2 = cntx.Genres.Include("Movies").Where(g => g.GenreID == 1).FirstOrDefault();
            //cntx.Dispose();
            //if (action2 != null)
            //{
            //    Console.WriteLine("{0} \t {1}", action2.Name, action2.Description);
            //    foreach (Movie filme in action2.Movies)
            //    {
            //        Console.WriteLine("\t{0}", filme.Title);
            //    }
            //}

            #endregion

            #region - consultas

            // MovieContext context = new MovieContext();
            // // filmes do diretor “Quentin Tarantino”
            // var query1 = from f in context.Movies
            //              where f.Director == "Quentin Tarantino"
            //              select f;

            // var query2 = from f in context.Movies
            //              where f.Director == "Quentin Tarantino"
            //              select f.Title;

            // var query3 = context.Movies
            //                       .Where(f => f.Director == "Quentin Tarantino")
            //                       .Select(f => f.Title);

            // Console.WriteLine("Filmes do diretor Quentin Tarantino");
            // foreach (String titulo in query2)
            // {
            //     Console.WriteLine(titulo);
            // }


            // //todos os filmes do genero "Action"
            // Console.WriteLine("\nFilmes de ação");
            // context.Database.Log = Console.Write;
            // var query4 = (from genero in context.Genres
            //                                    .Include("Movies")
            //               where genero.Name == "Action"
            //               select genero).First();

            // foreach (var filme in query4.Movies)
            // {
            //     Console.WriteLine("\t" + filme.Title);
            // }

            // //projeção sobre o título e dada de lançamento dos
            // //filmes do diretor “Quentin Tarantino” 
            // var query5 = from f in context.Movies
            //              where f.Director == "Quentin Tarantino"
            //              select new { f.Title, f.ReleaseDate };

            // foreach (var filme in query5)
            // {
            //     Console.WriteLine("{0}\t {1}", filme.ReleaseDate.ToShortDateString(), filme.Title);
            // }

            // // Gêneros ordenados pelo nome
            // var query6 = from g in context.Genres
            //              orderby g.Name descending
            //              select g;

            // foreach (var genero in query6)
            // {
            //     Console.WriteLine("{0}\t {1}", genero.Name, genero.Description);
            // }

            // //Filmes agrupados pelo ano de lançamento
            // var query7 = from f in context.Movies
            //              group f by f.ReleaseDate.Year;

            // foreach (var ano in query7.OrderByDescending(g => g.Key))
            // {
            //     Console.WriteLine("Ano: {0}", ano.Key);
            //     foreach (var filme in ano)
            //     {
            //         Console.WriteLine("\t{0:dd/MM}\t {1}",
            //                                  filme.ReleaseDate,
            //                                 filme.Title);
            //     }
            // }

            // //Projeção do faturamento total, quantidade de filmes
            // //e avaliação média agrupadas por gênero
            //var query8 = from f in context.Movies
            //             group f by f.Genre.Name into grpGen
            //             select new
            //             {
            //                 Categoria = grpGen.Key,
            //                 Filmes = grpGen,
            //                 Faturamento = grpGen.Sum(e => e.Gross),
            //                 Avaliacao = grpGen.Average(e => e.Rating),
            //                 Quantidade = grpGen.Count()
            //             };

            // foreach (var genero in query8)
            // {
            //     Console.WriteLine("Genero: {0}", genero.Categoria);
            //     Console.WriteLine("\tFaturamento total: {0}\n\t Avaliação média: {1}\n\tNumero de filmes: {2}",
            //                         genero.Faturamento, genero.Avaliacao, genero.Quantidade);
            //     Console.WriteLine("Filmes: ");
            //     foreach (var m in genero.Filmes)
            //     {
            //         Console.WriteLine("\t{0}", m.Title);
            //     }
            // }
            //
            #endregion

            #region - consultas com casting
            MovieContext cntx2 = new MovieContext();

            Console.WriteLine("\nElenco de Star Wars");
            var query9 = from p in cntx2.Characters.Include("Movie").Include("Actor")
                         where p.Movie.Title == "Star Wars"
                         select p;

            foreach (var res in query9)
            {
                Console.WriteLine("\t{0}\t {1}", res.Character, res.Actor.Name);
            }

            Console.WriteLine("\nAtores que desempenharam James Bond");
            var query10 = from p in cntx2.Characters.Include("Movie").Include("Actor") 
                         where p.Character == "James Bond"
                         orderby p.Movie.ReleaseDate.Year
                         select p;

            foreach (var res in query10)
            {
                Console.WriteLine("\t{0}\t {1}\t {2}", res.Movie.ReleaseDate.Year, res.Actor.Name, res.Movie.Title);
            }



            #endregion

            Console.ReadKey();


        }
    }
}

