using UnityEngine;
using Discord;
using System;

public class DiscordManager : MonoBehaviour
{
    private static DiscordManager instance;
    private Discord.Discord discord;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject); // Destroy duplicate instances
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject); // Persist this object across scenes

        discord = new Discord.Discord(1306218894930874448, (ulong)Discord.CreateFlags.Default); // Updated to Default
        UpdatePresence();
    }

    private void OnDisable()
    {
        if (discord != null)
        {
            discord.Dispose();
        }
    }

    public void UpdatePresence()
    {
        var activityManager = discord.GetActivityManager();

        var activity = new Discord.Activity
        {
            State = "Streaming on Discord", // Adjusted for clarity
            Details = "Horror Novel Gameplay",
            Timestamps =
            {
                Start = DateTimeOffset.UtcNow.ToUnixTimeSeconds() // Set dynamic start time for duration
            },
            Assets =
            {
                LargeImage = "img_0215",
                SmallImage = "img_0215",
                LargeText = "Happy Home",
                SmallText = "Rogue - Level 100"
            },
            Party =
            {
                Id = "ae488379-351d-4a4f-ad32-2b9b01c91657",
                Size = { CurrentSize = 1, MaxSize = 1 }
            },
            Secrets =
            {
                Join = "MTI4NzM0OjFpMmhuZToxMjMxMjM="
            }
        };

        activityManager.UpdateActivity(activity, (res) =>
        {
            if (res == Discord.Result.Ok)
            {
                Debug.Log("Discord presence updated successfully.");
            }
            else
            {
                Debug.LogError("Failed to update Discord presence: " + res);
            }
        });
    }

    private void Update()
    {
        if (discord != null)
        {
            discord.RunCallbacks();
        }
    }
}
