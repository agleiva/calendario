using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

public partial class MainWindow
{
    public MainWindow()
    {
        InitializeComponent();

        Buscar(2019);
    }

    private int Year() =>
        int.TryParse(Anio.Text, out var year)
            ? year
            : 0;

    private void Buscar(int? year = null)
    {
        if (year != null)
            Anio.Text = year.ToString();

        if (Year() != 0)
            DataContext =
                DiasDelAno(Year())
                    .GroupBy(x => x.Month)
                    .OrderBy(x => x.Key)
                    .Select(Mes)
                    .ToList();
    }

    public IEnumerable<DateTime> DiasDelAno(int ano)
    {
        var current = new DateTime(ano, 1, 1);

        while (current.Year == ano)
        {
            yield return current;
            current = current.AddDays(1);
        }
    }

    public List<DateTime?> Mes(IEnumerable<DateTime> mes) =>
        Enumerable.Repeat<DateTime?>(null, (int)mes.First().DayOfWeek)
                  .Concat(mes.Select(x => (DateTime?)x))
                  .ToList();

    private void Buscar_Click(object sender, RoutedEventArgs e) => Buscar();

    private void Inicio_Click(object sender, RoutedEventArgs e) => Buscar(1);

    private void Anterior_Click(object sender, RoutedEventArgs e) => Buscar(Math.Max(Year() - 1, 1));

    private void Siguiente_Click(object sender, RoutedEventArgs e) => Buscar(Math.Min(Year() + 1, 3000));

    private void Fin_Click(object sender, RoutedEventArgs e) => Buscar(3000);
}

