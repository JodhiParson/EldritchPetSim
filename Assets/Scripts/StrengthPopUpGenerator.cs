using TMPro;
using UnityEngine;

public class StrengthPopUpGenerator : MonoBehaviour
{
    public static StrengthPopUpGenerator current;
    public GameObject prefab;
    private void Awake()
    {
        current = this;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F10))
        {
            CreatePopup(Vector2.one, Random.Range(0, 1000).ToString());
        }
    }

    public void CreatePopup(Vector2 position, string text)
    {
        var popup = Instantiate(prefab, position, Quaternion.identity);
        var temp = popup.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        temp.text = text;

        Destroy(popup, 1f);
    }
}
