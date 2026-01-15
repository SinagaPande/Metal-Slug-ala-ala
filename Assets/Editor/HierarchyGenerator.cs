using UnityEngine;
using UnityEditor;
using System.Text;
using System.Linq;

public class HierarchyTextCopier : EditorWindow
{
    [MenuItem("GameObject/Copy Hierarchy as Text %#t")]
    public static void CopyHierarchyAsText()
    {
        StringBuilder sb = new StringBuilder();
        
        sb.AppendLine("// Entire Scene Hierarchy");
        sb.AppendLine();
        
        // Get all root objects in scene
        GameObject[] roots = UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();
        
        foreach (var root in roots)
        {
            BuildHierarchyText(root.transform, sb, 0);
        }
        
        // Copy to clipboard
        GUIUtility.systemCopyBuffer = sb.ToString();
        
        Debug.Log("Hierarchy copied as text to clipboard!");
        EditorUtility.DisplayDialog("Success", "Hierarchy copied as text to clipboard!", "OK");
    }
    
    private static void BuildHierarchyText(Transform transform, StringBuilder sb, int depth)
    {
        string indent = new string(' ', depth * 2);
        
        // Add current object with indent
        sb.Append($"{indent}├── {transform.name}");
        
        // Get all components except Transform
        var components = transform.GetComponents<Component>()
            .Where(c => c != null && c.GetType() != typeof(Transform) && c.GetType() != typeof(RectTransform))
            .ToList();
        
        // Format component list
        if (components.Count > 0)
        {
            sb.Append("  [");
            for (int i = 0; i < components.Count; i++)
            {
                string compName = components[i].GetType().Name;
                
                // Check if component is a MonoBehaviour (custom script)
                if (components[i] is MonoBehaviour)
                {
                    sb.Append($"{compName}.cs");
                }
                else
                {
                    sb.Append(compName);
                }
                
                if (i < components.Count - 1) sb.Append(", ");
            }
            sb.Append("]");
        }
        else
        {
            sb.Append("  []");
        }
        
        sb.AppendLine();
        
        // Recursively process children
        for (int i = 0; i < transform.childCount; i++)
        {
            BuildHierarchyText(transform.GetChild(i), sb, depth + 1);
        }
    }
    
    [MenuItem("Tools/Hierarchy Text Copier")]
    public static void ShowWindow()
    {
        GetWindow<HierarchyTextCopier>("Hierarchy Text Copier");
    }
    
    void OnGUI()
    {
        GUILayout.Label("Copy Hierarchy as Text", EditorStyles.boldLabel);
        
        if (GUILayout.Button("Copy Entire Scene as Text", GUILayout.Height(30)))
        {
            CopyHierarchyAsText();
        }
    }
}