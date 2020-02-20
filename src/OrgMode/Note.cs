using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace OrgMode
{
    public class Note
    {
        public int Level { get; set; }
        public string Title { get; set; }

        public List<Note> Notes { get; set; }
        public string Text;

        public Note()
        {
            this.Level = 1;
            Notes = new List<Note>();
        }

        public Note(string line) : this()
        {
            line = line.Trim();
            Title = line.TrimStart('*').Trim();
            var level = new Regex(Regex.Escape("*") + "*").Match(line).Length;
            Level = level;
        }
        
        public static Note Parse(string line)
        {
            var note = new Note(line); // 1 = top level
            return note;
        }

        public void AddLine(string nextLine)
        {
            if (!string.IsNullOrWhiteSpace(Text))
                Text += Environment.NewLine;
            Text += nextLine;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine(new string(Enumerable.Repeat('*', Level).ToArray()) + $" {Title}");
            foreach (var note in Notes)
            {
                sb.Append(note);
            }

            return sb.ToString();
        }
    }
}