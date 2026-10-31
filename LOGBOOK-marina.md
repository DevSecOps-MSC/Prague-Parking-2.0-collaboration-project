# Prague Parking V2.0 — Development Logbook

---

### Wednesday, 14 October

Our group, consisting of Marina, Stig, and Carl-Henrik, met to discuss our approach to the assignment.

We decided to approach Prague Parking V2.0 as a collaborative software development project, simulating a professional environment in which we work as colleagues within the same company.

During the meeting, we discussed which tools to use, how to organise our collaboration, and how to structure our workflow moving forward.

---

### Thursday, 15 October

We established a dedicated communication channel and created a shared Google Doc for brainstorming and documenting ideas.

Carl-Henrik worked on setting up a GitHub Organisation to host our shared repository and facilitate collaborative development. We also created a dedicated email account and named our organisation **DevSecOps-MSC**.

---

### Friday, 16 October

We met to discuss project milestones, scheduling study sessions, and establishing a collaborative workflow using Git and GitHub.

Our main objective is to divide responsibilities so that each member can work independently on different parts of the project while contributing to a shared codebase. This approach allows us to make progress simultaneously, minimise conflicts, and contribute according to our current skills and experience.

Stig also suggested that we begin considering the user interface (UI) as part of our development planning.

#### My Contributions
- Created and started maintaining my personal development logbook.
- Completed two introductory lessons on Git and GitHub to prepare for working with our shared repository.
- Cloned the repository locally and created my own development branch.

#### What I Learnt
I gained a better understanding of GitHub Organisations, repository management, and the workflow involved in cloning a shared repository, working on a separate branch, and eventually pushing contributions back to GitHub.

I decided to begin by working on the project documentation while continuing to develop the technical confidence needed to contribute to the codebase already established by Carl-Henrik and Stig.

#### Challenges and Questions
I am still familiarising myself with the project's existing structure and implementation. My main challenge is strengthening my understanding of the codebase so that I can contribute more independently and effectively.

#### Next Steps
Continue developing my technical knowledge, review the existing codebase, and gradually take on programming tasks that allow me to contribute more directly to the project's development.

---

### Saturday, 17 October

Troubleshooting GitHub pushing issues (local). I will commit and push again to check that everything works as it should.

I learnt a lot about Git and GitHub today. Troubleshooting was challenging, but I discovered that my push was blocked because I needed permission to access our organisation's repository. Once that was resolved, the process felt more intuitive. Stig also helped me understand branches and merging.

---

### Tuesday, 20 October

Today, I refreshed some of the programming theory needed for the assignment and fetched my colleagues' latest code from GitHub.

I am still working on understanding how the different parts of the programme connect. Tomorrow, I plan to review the updated code and, if I understand the relevant parts well enough, add my own contribution.

---

### Friday, 23 October

Today, I updated my local repository and tried to understand the group's testing workflow. At first, I thought I was missing some code because all the tests passed, but I later found out that Calle had already fixed them.

When new tests became available, I worked on the test checking that an empty parking space has all its capacity available. I changed `AvailableSpace` to return `Capacity`.

I accidentally committed and pushed this change directly to `main`. This helped me understand why I need to create and check my task branch before editing any code.

I am still finding parts of the theory difficult, but working through a specific test makes it easier to connect the concepts to the project.

---

### Saturday, 24 October

Today, I updated `main`, ran the tests and created a separate branch before making changes.

I worked on calculating the available space when vehicles are parked. I added a loop to total the sizes of the parked vehicles and subtract that total from the parking space's capacity.

The test still failed at first. After checking the related classes, I found that `RegisteredVehicle` received a size in its constructor but never stored it. I added `Size = size;` so the calculation could use the vehicle's actual size.

After both changes, the test passed and all previously passing tests still passed. Two other tests remained failing for separate tasks.

I then completed the branch workflow we implemented: committed and pushed my changes, created a pull request, merged it into `main`, updated my local copy and deleted the completed branch locally and on GitHub.

I still need practice with constructors, properties and Git terminology, but this task helped me understand how different classes work together and why a failing test can lead us to a problem in another part of the programme.

---

### Monday, 26 October

Today I continued working on the failing tests, trying to understand what was causing them.

I worked on the test for accessing a parking space by its ID. The problem was with the indexer, as parking space IDs start at 1 while collection indexes start at 0. I corrected the logic and updated the test so it checked the correct result.

I also worked on `FirstEmptySpace()`, using a `foreach` loop and the existing `IsEmpty` property to find the first empty parking space.

I followed the Git workflow for each fix, creating separate branches, committing, pushing and merging the changes into `main`.

We are getting close to the end of our project, so we also checked in with each other to see how we were feeling and started reflecting on our experience working together.

I think this project has made me very aware of the difference in knowledge and experience between us, as I am the least experienced in the group. So far, I feel that what I've become most confident with is Git and GitHub, while I still struggle quite a lot with programming logic.

I can understand parts of our project when I break them down and focus on individual methods or classes, but I still find it difficult to see how everything connects and understand the logic behind the programme as a whole.

I really admire Stig and Calle, and I'm genuinely happy I've had the opportunity to work alongside them. Their knowledge and the way they approach problems have motivated me to keep learning and improving.

---

### Tuesday, 27 October

Today I continued with the last failing test, `FirstAvailableSpace()`.

At first, I struggled to understand what the method needed to do, so I went through the logic step by step. I realised that the programme needed to check the remaining capacity of each parking space rather than just whether it was empty.

I used a `foreach` loop and compared the vehicle's size with `AvailableSpace`. The method now returns the ID of the first suitable space, or `-1` if none is available.

After testing, all 28 tests passed!

I then committed and pushed my changes, created a pull request, merged it into `main` and cleaned up my local branch.

I'm really happy to finally have some free time this evening to take care of myself and relax. Over the past week, I've felt quite a lot of pressure to do more, partly because I've been comparing my contributions to those of my colleagues and feeling that mine weren't as important.

At the same time, I know I've learnt a lot throughout this project. I'm much more comfortable with the Git workflow now, and I'm slowly starting to understand how to translate a requirement into actual code.

I still need practice with programming logic. A lot of practice!

---

### Thursday, 29 October

Today I updated my local repository and ran the new tests my colleagues had added.

I worked on counting cars and motorcycles within a parking space. I used `foreach` loops to check each parked vehicle's type and count the matching vehicles.

This helped me practice using `out` parameters. In these methods, the number of vehicles is passed back through `qty`, while the returned `bool` tells us whether any vehicles of that type were found.

Both tests passed, and all previously passing tests still passed. I committed and pushed each fix on its own branch, created pull requests and merged them into `main`.

I was tired, so I decided to stop after these two tasks and continue the next day.

---

### Friday, 30 October

Today I fetched the latest changes and continued working through the failing tests back to back!

At first, the programme crashed because my saved configuration only contained cars and motorcycles, while the new tests also needed bicycles. With Stig's magic, I understood that updating the code through Git does not automatically update the configuration saved separately on my computer. He fixed it and explained it, so everything worked as it should have.

Once that was resolved, I worked on counting bicycles within a parking space, moving vehicles between spaces, counting empty spaces and counting cars, motorcycles and bicycles across the whole garage.

For the garage totals, I reused the counting methods we already had for individual parking spaces and added their results together. This helped me understand how one method can use another instead of repeating the same logic.

For moving vehicles, I added the vehicle to the destination first and removed it from its original space only when parking succeeded.

I ran all tests after each change and followed our branch, commit, push and pull request workflow. By the end, all tests in the current suite passed, and my fixes were merged into `main`.

I still need practice to understand and write the logic independently, but these repeated counting tasks helped me recognise a pattern, so it is not as scary anymore and I am gaining some sort of speed in automation.

I am getting more comfortable breaking a task into smaller steps and checking the result before moving on.

---

### Saturday, 31 October

Today I updated my local repository and checked that all the existing tests still passed.

Calle was working on the menu and suggested that I help with the garage map. I used his earlier example as a starting point and created a separate `GarageMap` class.

The map shows each parking space's ID, with green for empty spaces, yellow for partly occupied spaces and red for full spaces. I kept the block symbols and connected the map to the garage used by the menu.

I checked the colours using a temporary motorcycle and car, then tested the layout with 100 parking spaces. Afterwards, I removed the temporary demonstration code.

When I created the pull request, there was a conflict because Calle had also updated `Menu.cs`. It was incredibly scary, as I had never stumbled upon a conflict. I kept his newer menu and restored the connections needed for my map. The combined programme built and displayed the map successfully, and I merged the pull request.

This was my first experience resolving a merge conflict in our shared code. I also learnt what the `.csproj` file does hand on, and practised passing an object to a constructor.

I still need help with parts of the code, but it was encouraging to contribute something more visible and tangible to the programme and build on the parking-space logic I had already worked with.

---