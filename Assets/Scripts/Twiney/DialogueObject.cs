using TMPro; // Import TextMesh Pro namespace
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueObject
{
    private const string kStart = "START";
    private const string kEnd = "END";

    public struct Response
    {
        public string displayText;
        public string destinationNode;

        public Response(string display, string destination)
        {
            displayText = display;
            destinationNode = destination;
        }
    }

    public class Node
    {
        public string title;
        public string text;
        public List<string> tags;
        public List<Response> responses;

        internal bool IsEndNode()
        {
            return tags.Contains(kEnd);
        }

        // Updated Print method to work with TextMeshProUGUI for displaying the node details
        public string Print()
        {
            // Example format for the node's text with responses
            string formattedText = $"Node Title: {title}\nTags: {string.Join(", ", tags)}\nText: {text}";

            // Add the responses to the print format
            foreach (var response in responses)
            {
                formattedText += $"\nResponse: {response.displayText} -> {response.destinationNode}";
            }

            return formattedText;
        }
    }

    public class Dialogue
    {
        string title;
        Dictionary<string, Node> nodes;
        string titleOfStartNode;

        public Dialogue(TextAsset twineText)
        {
            nodes = new Dictionary<string, Node>();
            ParseTwineText(twineText.text);
        }

        public Node GetNode(string nodeTitle)
        {
            return nodes[nodeTitle];
        }

        public Node GetStartNode()
       {
            UnityEngine.Assertions.Assert.IsNotNull(titleOfStartNode);
           return nodes[titleOfStartNode];
        }

        public void ParseTwineText(string twineText)
        {
            // Parsing logic remains unchanged
            // Your existing implementation goes here...
        }
    }
}
