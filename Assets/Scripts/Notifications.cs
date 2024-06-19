using System.Collections;
using Unity.Notifications.iOS;
using UnityEngine;

public class Notifications : MonoBehaviour
{
  private void Start()
  {
    StartCoroutine(RequestAuthorization());
  }

  private IEnumerator RequestAuthorization()
  {
    var authorizationOption = AuthorizationOption.Alert | AuthorizationOption.Badge;
    using (var req = new AuthorizationRequest(authorizationOption, true))
    {
      while (!req.IsFinished)
      {
        yield return null;
      }

      if (req.Granted)
      {
        iOSNotificationCenter.RemoveAllScheduledNotifications();
        var timeTrigger = new iOSNotificationTimeIntervalTrigger()
        {
          TimeInterval = new System.TimeSpan(7, 0, 0, 0),
          Repeats = true
        };
        
        string title = "Time to play!";
        string body = "New levels are waiting for you";

        if (Application.systemLanguage == SystemLanguage.Russian)
        {
          title = "Время поиграть!";
          body = "Новые уровни тебя уже ждут!";
        }

        var notification = new iOSNotification()
        {
          Identifier = "_notification_01",
          Title = title,
          Body = body,
          ShowInForeground = true,
          ForegroundPresentationOption = (PresentationOption.Alert | PresentationOption.Sound),
          CategoryIdentifier = "category_a",
          ThreadIdentifier = "thread1",
          Trigger = timeTrigger,
        };

        iOSNotificationCenter.ScheduleNotification(notification);
      }
    }
  }
}
