// using UnityEngine;
// using UnityEngine.UI;
//
// public class MoodBar : MonoBehaviour
// {
//
//     float _lerpSpeed;
//     float _mood;
//     float _maxMood;
//     public Image moodBar;
//     public Text moodText;
//     ManagerMood _managerMood;
//
//     public void Awake()
//     {
//         _managerMood = GameObject.Find("ManagerMood").GetComponent<ManagerMood>();
//     }
//     public void Update()
//     {
//         _maxMood = _managerMood.maxPoints;
//         _mood = _managerMood.points;
//         MoodBarFiller();
//         ColorChanger();
//         Text();
//     }
//     public void MoodBarFiller()
//     {
//         _lerpSpeed = 3f * Time.deltaTime;
//         moodBar.fillAmount = Mathf.Lerp(moodBar.fillAmount, _mood / _maxMood, _lerpSpeed);
//     }
//     public void ColorChanger()
//     {
//         Color moodColor = Color.Lerp(Color.black, Color.white, (_mood / _maxMood));
//         moodBar.color = moodColor;
//     }
//     public void Text()
//     {
//         moodText.text = "Sanity: " + _mood + "%";
//     }
// }


