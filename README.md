# org-mode
C# Org Mode Library. 

```markdown
NotebookTitle

* firstNote
firstNodeContents

** firstChild
** secondChild

secondChildContents
* TODO secondNote

```
```c#
var notebook = OrgMode.Parse(orgFilePath)
notebook.Notes.First.
```
