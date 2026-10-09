using UnityEngine;
using UnityEngine.UIElements;

public class ButtonManager : MonoBehaviour
{
    private UIDocument uiDocument;
    public Button trigger;
    public Button inventory;
    public Button ae;
    public Button settings;
    public Button trion;
    public Button Leave;
    public Button exit;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
public class LevelSelectController : MonoBehaviour
{
    void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        // Find every button inside the UI document
        UQueryBuilder<Button> allButtons = root.Query<Button>();

        // Loop through each button and register a unified method
        allButtons.ForEach(button =>
        {
            button.clicked += () => OnLevelButtonClicked(button);
        });
    }

    private void OnLevelButtonClicked(Button button)
    {
        // Process the logic based on the button's name or custom attributes
        Debug.Log($"Loading level layout from: {button.name}");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
