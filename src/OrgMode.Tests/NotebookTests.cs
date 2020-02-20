using System;
using System.Collections.Generic;
using System.IO;
using FluentAssertions;
using Xunit;

namespace OrgMode.Tests
{
    public class NotebookTests
    {
        [Fact]
        public void Parse_HasTitleAndNestedNotes()
        {
            var notebook = Notebook.Parse(@"
notebookTitle
* firstNote
firstNoteContents
** firstNoteChild1
");
            var dump = notebook.ToString();
            System.Console.Write(dump);
            
            notebook.Should().BeEquivalentTo(new Notebook()
            {
                Title = "notebookTitle",
                Notes = { new Note
                    {
                        Title = "firstNote",
                        Text = "firstNoteContents",
                        Notes = new List<Note> {
                            new Note { Level = 2, Title = "firstNoteChild1" }
                        }
                    },
                    //new Note() { Title = "secondNote"}
                }
            });
        }
    }
}
