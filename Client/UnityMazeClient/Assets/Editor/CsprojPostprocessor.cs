using UnityEditor;
using UnityEngine;

public class CsprojPostprocessor : AssetPostprocessor
{
    public static string OnGeneratedCSProject(string path, string content)
    {
        Debug.Log($"[CsprojPostprocessor] Called for: {path}");

        if (!path.EndsWith("Assembly-CSharp.csproj"))
            return content;

        Debug.Log("[CsprojPostprocessor] Patching Assembly-CSharp.csproj");

        const string insertion =
            "    <Reference Include=\"Maze.Common\">\n" +
            "      <HintPath>Assets\\Plugins\\Maze.Common.dll</HintPath>\n" +
            "    </Reference>\n";

        string patched = content.Replace(
            "  </ItemGroup>\n</Project>",
            "  " + insertion + "  </ItemGroup>\n</Project>"
        );

        Debug.Log($"[CsprojPostprocessor] Content changed: {patched != content}");

        return patched;
    }
}