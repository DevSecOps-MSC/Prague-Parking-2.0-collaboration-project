# Developer Log

### Stig Rudeholm

## 2026-10-14

I was asked by Marina and Carl-Henrik if I would like to work on this assignment `Prague Parking v2` together, as a group. As I have never worked on a software project as part of a group, I was hesitant at first. But it seemed like a great chance to try something a little bit outside of my comfort zone, so I decided to go for it!

We started off with a brainstorming session where we talked about what tools to use, and also just got to know each other better in general. Looks like we have a lot in common, so I have every confidence that we will be able to pull this off!

---

## 2026-10-15

We set up a shared Google document and started writing down thoughts and ideas about the structure of the project.

Carl-Henrik set up an organization on GitHub, so we can all work on a shared repository.

We all need to get more comfortable with how Git and GitHub works, so we shared tips about available online resources, like the free courses on `codecademy.com`.

I have prior experience of Git and GitHub, but only very superficially and only for solo developing. It will probably take some time to get into the collaborative workflow needed for this project.

---

## 2026-10-16

Spent a lot of time wrestling with GitHub, wrangling branches and getting to know what to do and in what order.

Started writing some basic Classes, basically trying to map the datastructures needed for the application. They will definitely change, but it's a start. Finally managed to create my first pull request and merge a branch into `main`.

I am learning a lot and I am enjoying myself!

---

## 2026-10-17

Copied my rudimentary test suite from my `Prague Parking v1.1` solution and adapted it, crafting it into it's own class called `Simple Testing`.

Wrote some small tests to try it out and it seems to work as intended. Not sure if we'll use it, but it could be a neat little tool to help steer the development process.

Will also look into "proper" testing in C# when I start work on our file handling Class Library.

---

Made the switch to VSCode on Linux. Some things are a bit fiddlier than in Visual Studio proper, but overall I am very happy with my decision.

---

Started work on our file handler Class Library that will be used to load and save configuration and parking garage state. Set up an MSTest project alongside it, to do test-driven development. Not sure if it's worth it, but I think it can be. I'm certainly learning a lot, which is the whole point anyway.

---

## 2026-10-18

Decided to rewrite the `FileHandler` library. I was overthinking it. Maybe I still am... I've narrowed my focus and will just do a simple wrapper around certain methods in the `File` class, to load and save text files from/to disk.

Starting to get into the circular Git workflow:

+ `Pull` to sync `main`
+ Create a `new branch` and `checkout`
+ Work on stuff, remember to `commit` often
+ `Push` branch to GitHub (keep changes few and/or small, to avoid potential conflicts)
+ Initiate `pull request` on GitHub
+ `Merge` branch into `main`
+ Delete branch
+ Go back to beginning and do it again

---

Started working on a `Config` class. Reading up on `JSON` serialization and deserialization. Shouldn't be too difficult to figure out the basics.

---

Basics was easy, but I can't seem to get `JsonSerializer` to include the two `Dictionary` properties:

```
Dictionary<VehicleType, int> VehicleSizes
Dictionary<VehicleType, decimal> Rates
```

---

## 2026-10-19

Turned out it was far less complicated than I thought. I had simply forgotten to make the `VehicleType` enum *public*! Now I need to get deserialization down.

---

I had to fiddle quite a bit, but again it really turned out to be less complicated than I thought.

To include properties with private setters, use the `JsonInclude` attribute:

```
[JsonInclude]
public Dictionary<VehicleType, int> VehicleSizes { get; private set; } = [];
```

And to properly deserialize into a `Config` object, include a constructor with no arguments:

```
public Config() { }
```

---

***We can now load and save configuration files!***

```
var configFilePath = "../../../config.json";

// Load //
var jsonString = FileHandler.Load(configFilePath);
var config = Config.FromJson(jsonString);

// Save //
var jsonString = config.ToJson();
FileHandler.Save(configFilePath, jsonString);
```

---

## 2026-10-20

Didn't get much done, but started re-writing my simple testing system. Thinking of removing `FileHandlerLibraryTest` because the `FileHandlerLibrary` turned out to be so much simpler than I thought it would be. Maybe turn `SimpleTest` into its own class library and use it like I did in `v1.1`?

---

## 2026-10-21

Continuing the re-write of `SimpleTest` today. It's starting to look pretty good to me, and I think turning it into a proper class library might be a good idea.

---

Converting `SimpleTest` the project into a proper class library was super easy, barely an inconvenience. I am officially making it a part of the overall solution. And removing a bunch of chaff.

---

I must confess I'm feeling a bit lost in this project at the moment. The way I usually find my way back is by writing tests and making them pass, so that's what I am going to do. Except that I will try to write more tests than I solve myself, to help both me and my partners to focus and answer the "what's next?" question.

---

## 2026-10-22

I've been wrestling with some of the data models, but now I think it's moving in the right direction. Here are my plans for the pseudo-TDD suggested above.

---

### Rules (actually, they're more *guidelines* than actual rules...)

- A test should be made to pass by writing only the code needed to make it pass.
- Tests go from general to more specific in scope, but our code should go from specific to more general.
- There are several categories of tests.
- Tests should be tackled *in order* within each category.

### Workflow

I suggest the following workflow for each test, no matter how small addition needed to make it pass:
- Run all tests.
- Pick the *first failing* test in any category.
- Checkout a new branch in Git.
- Make the test pass.
    - Edit code
    - Run all tests
    - If test still fails (red), try again
    - If test passes (turns green) continue below
- Commit, with the name/description of the test as the Git commit message.
- Push to GitHub.
- Create Pull Request.
- If there are no conflicts, merge into main branch and delete the extra branch.
- Start over.

