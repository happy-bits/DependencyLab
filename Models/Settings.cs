namespace DependencyLab.Models;

public static class Settings
{
    public static RepositoryChoise RepositoryChoise => RepositoryChoise.Csv;
}

public enum RepositoryChoise
{
    InMemory, Csv, Json
}