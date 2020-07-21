using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Delegates.Reports
{
    public abstract class Format
    {
        public Func<string, string> MakeCaption { get; protected set; }
        public Func<string> BeginList { get; protected set; }
        public Func<string, string, string> MakeItem { get; protected set; }
        public Func<string> EndList { get; protected set; }
    }

    public class HtmlFormat : Format
    {
        public HtmlFormat()
        {
            MakeCaption = caption => $"<h1>{caption}</h1>";
            BeginList = () => "<ul>";
            MakeItem = (valueType, entry) => $"<li><b>{valueType}</b>: {entry}";
            EndList = () => "</ul>";
        }
    }

    public class MarkDownFormat : Format
    {
        public MarkDownFormat()
        {
            MakeCaption = caption => $"## {caption}\n\n";
            BeginList = () => "";
            MakeItem = (valueType, entry) => $" * **{valueType}**: {entry}\n\n";
            EndList = () => "";
        }
    }

    public abstract class Stat
    {
        public string Caption { get; protected set; }
        public Func<IEnumerable<double>, object> MakeStatistics { get; protected set; }
    }

    public class MeanAndStdStat : Stat
    {
        public MeanAndStdStat()
        {
            Caption = "Mean and Std";
            MakeStatistics = (data) =>
            {
                var listData = data.ToList();
                var mean = listData.Average();
                var std = Math.Sqrt(listData.Select(z => Math.Pow(z - mean, 2)).Sum() / (listData.Count - 1));

                return new MeanAndStd
                {
                    Mean = mean,
                    Std = std
                };
            };
        }
    }

    public class MedianStat : Stat
    {
        public MedianStat()
        {
            Caption = "Median";
            MakeStatistics = (data) =>
            {
                var list = data.OrderBy(z => z).ToList();
                if (list.Count % 2 == 0)
                    return (list[list.Count / 2] + list[list.Count / 2 - 1]) / 2;
                else
                    return list[list.Count / 2];
            };
        }
    }

    public class ReportMaker
	{
		public string MakeReport(IEnumerable<Measurement> measurements, Stat stat, Format format)
		{
			var data = measurements.ToList();
			var result = new StringBuilder();
			result.Append(format.MakeCaption(stat.Caption));
			result.Append(format.BeginList());
			result.Append(format.MakeItem("Temperature", stat.MakeStatistics(data.Select(z => z.Temperature)).ToString()));
			result.Append(format.MakeItem("Humidity", stat.MakeStatistics(data.Select(z => z.Humidity)).ToString()));
			result.Append(format.EndList());
			return result.ToString();
		}
	}

	public static class ReportMakerHelper
	{
		public static string MeanAndStdHtmlReport(IEnumerable<Measurement> data)
		{
			return new ReportMaker().MakeReport(data, new MeanAndStdStat(), new HtmlFormat());
		}

		public static string MedianMarkdownReport(IEnumerable<Measurement> data)
		{
			return new ReportMaker().MakeReport(data, new MedianStat(), new MarkDownFormat());
		}

		public static string MeanAndStdMarkdownReport(IEnumerable<Measurement> measurements)
		{
            return new ReportMaker().MakeReport(measurements, new MeanAndStdStat(), new MarkDownFormat());
        }

		public static string MedianHtmlReport(IEnumerable<Measurement> measurements)
		{
            return new ReportMaker().MakeReport(measurements, new MedianStat(), new HtmlFormat());
        }
	}
}