using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileScript : MonoBehaviour
{
    
    // Test dialogue
    public List<string> testDialogue1 = new List<string> {
        "Well... this is getting harder, isn't it?",
        "Don't worry, I'm sure you'll get it soon",
        "Maybe... maybe not. Hopefully yes!"
    };
    public List<string> testDialogue2 = new List<string> {
        "Let's get that search function tested!",
        "I wonder if this will be picked up?",
        "If you're seeing this, the answer is yes!"
    };
    public List<string> testDialogue3 = new List<string> {
        "The end of the line is now.",
        "Okay, maybe that's a bit too dramatic.",
        "Maybe... maybe not. Hopefully yes!"
    };

    // Tutorial dialogues
    public List<string> relax1Tutorial = new List<string> {
        "Welcome to relaxation module 1... bit rain!",
        "There's uh... not much of a tutorial to give for this one",
        "Just sit back and enjoy the spectacle... relax!",
        "So... good luck! Where I come from, this is what weather is like!"
    };
    public List<string> relax2Tutorial = new List<string> {
        "Welcome to relaxation module 2... pixel painter!",
        "Here you can make basic pixel art to your heart's content",
        "There's three tools: the brush, the eraser, and the trash",
        "Click the eraser to enable erase mode, or the brush to disable it",
        "The trash erases everything on screen, so be careful!",
        "Tap any of the colored squares to swap your brush to that color",
        "So... good luck! You should try making art of me!"
    };
    public List<string> relax3Tutorial = new List<string> {
        "Welcome to relaxation module 3... sorting audiovisualizer!",
        "Here, you can pick between three basic sorting algorithms",
        "Click a button, and you'll see that algorithm in real time!",
        "Well, see AND hear it... ahhhh, how I love the sound",
        "So... good luck! I hope you enjoy it as much as I do!"
    };
    public List<string> focus1Tutorial = new List<string> {
        "Welcome to focus module 1... dice holdem!",
        "Placeholder"
    };
    public List<string> focus2Tutorial = new List<string> {
        "Welcome to focus module 2... napsack catch!",
        "Placeholder"
    };
    public List<string> focus3Tutorial = new List<string> {
        "Welcome to focus module 3... maze navigator!",
        "Placeholder"
    };
    public List<string> memory1Tutorial = new List<string> {
        "Welcome to memory module 1... answer before question quiz!",
        "It is aptly named, as you will see a set of answers before the question",
        "You must memorize these answers before the question appears!",
        "Once the question appears, the answers will be hidden from you",
        "The correct answer is highlighted in green, whether win or lose",
        "Easy, medium, and hard only dictate how long the questions are shown for",
        "So... good luck! All these questions use C# as their coding language!"
    };
    public List<string> memory2Tutorial = new List<string> {
        "Welcome to memory module 2... variable memorization!",
        "Variable in this case refers to the coding concept of variables!",
        "Once you select a difficulty, you will be given starting variables",
        "On easy, you get 1, on medium, you get 2, and on hard, you get 3",
        "Memorize these numbers quickly, as they will soon vanish",
        "Afterwards, you will see a random one go through a random operation",
        "Keep track of how this would modify the original number",
        "Once finished, you will be asked for the final value of one of them",
        "The correct answer is highlighted in green, whether win or lose",
        "So... good luck! Division here is always integer division!"
    };

    public List<string> memory3Tutorial = new List<string> {
        "Welcome to memory module 3... code language matching!",
        "This is a typical card matching memory game, but with a twist!",
        "You are not matching the same cards, but the same language!",
        "The code snippet on the card is one of four langauges",
        "They are Java, JavaScript, C#, and Python",
        "You must pick two code snippets from the same language",
        "If you successfully pick two from the same language, they vanish",
        "Make all cards vanish and you win!",
        "Difficulty dictates your allowed number of wrong answers",
        "You get 2 wrong answers on easy, 1 on medium, and 0 on hard",
        "So... good luck! Hope you know your code languages well!"
    };
    public List<string> logic1Tutorial = new List<string> {
        "Welcome to logic module 1... multi converter!",
        "This one is a utility, as well as a way to memorize conversions!",
        "First, type a binary, hexadecimal, ASCII, or decimal code into the box",
        "Then, check the box of the code type you entered in!",
        "You will then see it converted to the other 3 types before your eyes!",
        "Ever wondered what your favorite letter was in binary? Or hex?",
        "So... good luck! Test out some combinations, see if you can remember!"
    };
    public List<string> logic2Tutorial = new List<string> {
        "Welcome to logic module 2... cipher guesser!",
        "You should see a weird, scrambled looking sentence... that's normal!",
        "Your job is to figure out the cipher code that made it",
        "Just remember, it's all caesar cipher: answers are always 1-25",
        "Letters are shifted down the alphabet based on the cipher code",
        "So if a letter is a and it should be b, that means the answer is 1!",
        "Every sentence is a quote, the source of the quote given to you",
        "Use these to figure out what the sentence might be, then the code!",
        "So... good luck! Hope you know your movie media and alphabet!"
    };
    public List<string> logic3Tutorial = new List<string> {
        "Welcome to logic module 3... gates of truth!",
        "Dramatic name I know, but it is the truth... hehe",
        "You will see a table of true and false combinations, this is constant",
        "What changes is the formula in the top right, the truth operation",
        "Based on this operation, you need to type the results of the table",
        "As the text says, type in XXXX format, like TFTF",
        "~ is negation, ^ is and, v is or....",
        "⊕ is xor, ⊙ is xnor....",
        "So... good luck! Use some logic for these gates!"
    };

    // Easter egg dialogue
    public static List<string> Egg1 = new List<string> {
        "We're... not even in a module, you know that?",
        "I mean, I'm happy you like the menus, but....",
        "Don't you have something better to do with your time?"
    };
    public static List<string> Egg2 = new List<string> {
        "You know... the app's name is a pun of requiescence",
        "Mouthful of a word, but get it? RequiiCSense?",
        "Requii-CS-ense. Computer Science. Good, right?"
    };
    public static List<string> Egg3 = new List<string> {
        "Fun fact: I am a computer mouse. Mouse in a computer",
        "Double fun fact: I am actually just white pixels",
        "Triple fun fact... nevermind, too existential now"
    };
    public static List<string> Egg4 = new List<string> {
        "For those who don't know, Unity uses C# language",
        "That's why it's the default for memory module one",
        "Answer before question quiz was the first developed!"
    };
    public static List<string> Egg5 = new List<string> {
        "Congratulations, you are [1,000th customer]!",
        "Click the Requii button again for a [FREE PRIZE]!",
        "Nah, not really... your only prize is knowledge!"
    };

    public List<List<string>> Eggs = new List<List<string>>{
        Egg1,
        Egg2,
        Egg3,
        Egg4,
        Egg5
    };

}
