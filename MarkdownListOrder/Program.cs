using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

public static class Program
{
	private const string NEW_LINE = "\r\n";

	private static readonly Regex DISP_TEXT_REGEX = new Regex( @"- \[(.*)\]\((.*)\) - (.*)" );

	private sealed class Data
	{
		public readonly string m_text;
		public readonly string m_dispText;

		public Data( string text )
		{
			m_text     = text;
			m_dispText = DISP_TEXT_REGEX.Replace( text, "- $1 - $3" );
		}
	}

	[STAThread]
	private static void Main()
	{
		var lines = Clipboard
				.GetText()
				.Split( new[] { NEW_LINE }, StringSplitOptions.RemoveEmptyEntries )
				.Select( c => new Data( c ) )
				.OrderBy( c => c.m_dispText.Length )
			;

		var result = string.Join( NEW_LINE, lines.Select( c => c.m_text ) );

		Clipboard.SetText( result );
	}
}