var target = Argument("target","test");
var configuration = Argument("configuration","Release");
var solutionFolder = "../Phonebook.sln";

class ProjectInformation
{
    public string Name { get; set; }
    public string FullPath { get; set; }
    public bool IsTestProject { get; set; }
}

List<ProjectInformation> projects;
Setup(context=> 
{
	    projects = GetFiles("../src/**/*.csproj").Select(p => new ProjectInformation
		{
			Name = p.GetFilenameWithoutExtension().ToString(),
			FullPath = p.GetDirectory().FullPath,
			IsTestProject = p.GetFilenameWithoutExtension().ToString().EndsWith(".Tests")
		}).ToList();

		foreach(var project in projects)
        {
            Information(project.Name);
        }
});


Task("restore")
	.Does(()=> 
	{
		DotNetRestore(solutionFolder);
	});


Task("build")
	.IsDependentOn("restore")
	.Does(()=> 
	{
		DotNetBuild(solutionFolder, new DotNetBuildSettings
		{
			NoRestore = true,
			Configuration = configuration
		});
	});

Task("test")
	.IsDependentOn("build")
	.Does(()=> {
		DotNetTest(solutionFolder, new DotNetTestSettings
		{
			NoRestore = true,
			NoBuild = true,
			Configuration = configuration
		});
	});


RunTarget(target);