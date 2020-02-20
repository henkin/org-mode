using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OrgMode
{
    public class Notebook
    {
        public string Title { get; set; }
        public List<Note> Notes { get; }

        public Notebook()
        {
            Notes = new List<Note>();
        }
        public static Notebook Parse(string notebookText)
        {
            var notebook = new Notebook();
            
            using (var reader = new StringReader(notebookText))
            {
                var nextLine = reader.ReadLine();
                while (!IsNote(nextLine))
                {
                    notebook.Title += nextLine;
                    nextLine = reader.ReadLine();
                }

                Note currentNote = null;

                do
                {
                    if (IsNote(nextLine))
                    {
                        if (currentNote != null)
                        {
                            // FIRST  note
                            var newNote = Note.Parse(nextLine);
                            if (newNote.Level > currentNote.Level)
                                currentNote.Notes.Add(newNote);
                            currentNote = newNote;
                        }
                        else
                        {
                            // FIRST  note
                            currentNote = Note.Parse(nextLine);
                        }
                       
                        // if first level, just add to notebook
                        if (currentNote.Level == 1)
                            notebook.Notes.Add(currentNote);
                    }
                    else
                    {
                        currentNote.AddLine(nextLine);
                    }
                } while ((nextLine = reader.ReadLine()) != null);
            }

            return notebook;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine(Title);
            foreach (var note in Notes)
            {
                sb.Append(note);
            }

            return sb.ToString();
        }

        private static bool IsNote(string nextLine)
        {
            return nextLine.StartsWith("*");
        }
    }
}
