using UnityEditor;

[CustomEditor(typeof(PortalScript)), CanEditMultipleObjects]
public class PortalEditor : Editor {
  public enum DisplayCategory { Normal, Speed, Gravity, Gamemode, FinishLevel }
  public DisplayCategory categoryToDisplay;

  bool FirstTime = true;

  public override void OnInspectorGUI() {
    if (FirstTime) {
      switch (serializedObject.FindProperty("State").intValue) {
        case 0:
          categoryToDisplay = DisplayCategory.Normal;
          break;
        case 1:
          categoryToDisplay = DisplayCategory.Speed;
          break;
        case 2:
          categoryToDisplay = DisplayCategory.Gravity;
          break;
        case 3:
          categoryToDisplay = DisplayCategory.Gamemode;
          break;
        case 4:
          categoryToDisplay = DisplayCategory.FinishLevel;
          break;
      }
    } else
      categoryToDisplay = (DisplayCategory)EditorGUILayout.EnumPopup(
          "Display", categoryToDisplay);

    EditorGUILayout.Space();

    switch (categoryToDisplay) {
      case DisplayCategory.Normal:
        DisplayProperty("Normal", 0);
        break;
      case DisplayCategory.Speed:
        DisplayProperty("Speed", 1);
        break;
      case DisplayCategory.Gravity:
        DisplayProperty("Gravity", 2);
        break;
      case DisplayCategory.Gamemode:
        DisplayProperty("Gamemode", 3);
        break;
      case DisplayCategory.FinishLevel:
        DisplayProperty("Level", 4);
        break;
    }

    FirstTime = false;
    serializedObject.ApplyModifiedProperties();
  }

  void DisplayProperty(string property, int PropNumb) {
    try {
      EditorGUILayout.PropertyField(serializedObject.FindProperty(property));
    } catch {}
    serializedObject.FindProperty("State").intValue = PropNumb;
  }
}
