# Prague Parking V2.0 — Development Logbook

### Wednesday, 14 October

Our group, consisting of Marina, Stig and Carl-Henrik, met to discuss the assignment, development tools and organisation of the project. We agreed to work collaboratively on a shared codebase, using a workflow based on separate tasks and contributions.

---

### Thursday, 15 October

We established a dedicated communication channel and a shared Google Doc for brainstorming and planning. Carl-Henrik worked on setting up the GitHub Organisation and shared repository. We also created a dedicated email account and named the organisation **DevSecOps-MSC, for Marina, Stig and Calle.**

---

### Friday, 16 October

We discussed milestones, study sessions and the Git and GitHub workflow. We planned to divide responsibilities so that development could proceed on separate tasks within the shared codebase. Stig also raised UI planning.

I created the development logbook, completed two introductory Git and GitHub lessons, cloned the repository and created a development branch. I began reviewing the project structure and documentation.

---

### Saturday, 17 October

I investigated a push failure and identified missing access permissions for the organisation’s repository. The permissions issue was resolved. I also reviewed the branch and merge workflow with Stig.

---

### Tuesday, 20 October

I fetched the latest changes from GitHub and reviewed programming concepts relevant to the assignment. The next planned step was to review the updated implementation and identify a programming task.

---

### Friday, 23 October

I updated my local repository and reviewed the group’s testing workflow. Carl-Henrik’s existing fixes were already passing the available tests.

When additional tests became available, I worked on the requirement that an empty parking space has its full capacity available. I changed `AvailableSpace` to return `Capacity`. This was the initial implementation, before accounting for parked vehicles in the next task.

---

### Saturday, 24 October

I updated `main`, ran the tests and created a task branch. I extended `AvailableSpace` to total the sizes of parked vehicles and subtract that total from the parking space’s capacity.

While investigating the failing test, I found that `RegisteredVehicle` received a size in its constructor without storing it. I added `Size = size;` so the calculation could use the vehicle’s size.

The relevant test and all previously passing tests passed after these changes. Two other tests remained failing for separate tasks. I committed and pushed the changes, created and merged a pull request, updated my local `main` and deleted the completed branch.

---

### Monday, 26 October

I corrected the indexer logic for accessing a parking space by ID: space IDs start at 1, while collection indexes start at 0. I also updated the corresponding test to check the expected result.

I implemented `FirstEmptySpace()` using a `foreach` loop and the existing `IsEmpty` property. Each fix followed the separate-branch, commit, push and pull-request workflow before merging into `main`.

---

### Tuesday, 27 October

I implemented `FirstAvailableSpace()` by comparing the vehicle’s size with each parking space’s `AvailableSpace`. The method returns the ID of the first suitable space, or `-1` if none is available.

All 28 tests in the suite passed at this point. I committed and pushed the change, merged the pull request and cleaned up the completed branch.

---

### Thursday, 29 October

I updated my local repository and ran the new tests. I implemented counting cars and motorcycles within a parking space using `foreach` loops and vehicle-type checks.

Each method supplies the count through the `out` parameter `qty` and returns a `bool` indicating whether any matching vehicles were found. Both relevant tests and all previously passing tests passed. I committed, pushed and merged each fix through a separate branch and pull request.

---

### Friday, 30 October

I fetched the latest changes and continued with the remaining failing tests. An initial configuration issue occurred because the locally saved configuration contained cars and motorcycles, while the new tests also required bicycles. Stig resolved the issue and explained the distinction between repository updates and configuration stored separately on the computer.

I implemented bicycle counting within a parking space, vehicle movement between spaces, empty-space counting and garage-wide totals for cars, motorcycles and bicycles. The garage totals reuse the individual parking-space counting methods and sum their results.

For vehicle movement, the implementation attempts to park in the destination first and removes the vehicle from its original space only when that succeeds.

I ran the tests after each change and used separate branches and pull requests. All tests in the current suite passed by the end of the session, and the fixes were merged into `main`.

---

### Saturday, 31 October

I updated my local repository and verified that the existing tests passed. While Carl-Henrik worked on the menu, I used his earlier example as a starting point for a separate `GarageMap` class.

The map displays parking-space IDs and indicates empty, partly occupied and full spaces using green, yellow and red block symbols. It reads the garage instance supplied to the menu.

I checked the three occupancy states using a temporary motorcycle and car, and checked the layout with 100 parking spaces. I removed the temporary demonstration code afterwards.

Concurrent changes to `Menu.cs` caused a merge conflict. I retained Carl-Henrik’s newer menu implementation and restored the map connections. The combined programme built and displayed the map successfully, and the pull request was merged.

---

### Sunday, 1 November

We met to discuss the remaining adjustments before submission. I also revised my development logbook to check completed tasks, technical changes and testing.

The project is nearing completion. We plan to meet once more to review the final changes before updating the shared repository. 