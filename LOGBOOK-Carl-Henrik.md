Carl-Henrik Alm – Development Log
2026-10-14
Done:
Participated in the initial brainstorming session together with Marina and Stig.
Discussed project requirements, development tools and possible project structure.
Explored options for collaboration and source control.
Working on:
Understanding project scope and team workflow.
Next step:
Establish collaboration tools and repository structure.
2026-10-15
Done:
Created the GitHub Organisation and project repository.
Helped establish the group's collaboration workflow.
Set up supporting communication and documentation channels.
Began learning collaborative Git and GitHub workflows.
Working on:
Git branching, pull requests and repository management.
Next step:
Configure local development environment and begin exploring the application structure.
2026-10-16
Done:
Completed introductory Git and GitHub learning resources.
Cloned the repository and created development branches.
Began reviewing the project architecture and codebase.
Started the project development log.
Working on:
Understanding the overall application structure.
Next step:
Identify suitable frontend responsibilities.
2026-10-17 – 2026-10-25
Done:
Participated in project discussions and design decisions.
Reviewed updates from the team and followed backend development.
Continued practising collaborative Git workflows.
Familiarised myself with the project architecture and planned frontend responsibilities.
Working on:
UI concepts and workflow design.
Next step:
Begin implementation of the menu system.
2026-10-26
Done:
Created the first implementation of the application menu.
Started building the user interface using Spectre.Console.
Planned the layout for menu, map, user interaction area and system log.
Working on:
User interface structure and navigation.
Next step:
Expand menu functionality and improve layout design.
2026-10-29
Done:
Added the Garage Map to the user interface.
Integrated menu navigation with the map presentation.
Continued refining the application layout.
Merged menu-related work into the main branch.
Working on:
Interactive UI components.
Next step:
Create input flows and interaction panels.
2026-10-30
Done:
Implemented interactive menu panels.
Added correctly sized tables and layout sections.
Added dynamic header functionality.
Implemented vehicle type selection.
Implemented vehicle registration input handling.
Continued improving the overall menu workflow.
Working on:
Connecting menu actions to future backend functionality.
Next step:
Implement individual workflows for Parking, Retrieve, Search and Move.
2026-10-31
Done:
Refactored Menu.cs by replacing multiple booleans with the MenuMode enum.
Implemented structured menu state handling.
Added Retrieve input workflow.
Added Move input workflow.
Implemented List View navigation.
Added Admin menu navigation.
Improved menu maintainability and readability.
Working on:
Backend integration.
Next step:
Connect workflows to backend classes and methods.
2026-11-01
Done:
Added Save, Load and Config options to the Admin menu.
Improved code readability through comments and cleanup.
Moved MoveFromSpace and MoveToSpace into separate input blocks.
Connected Save functionality to garage data storage.
Connected Load functionality to garage data storage.
Fixed integration issues related to Save and Load.
Connected Parking workflow to backend methods.
Connected Retrieve workflow to backend methods.
Connected Search workflow to backend methods.
Connected Move workflow to backend methods.
Replaced placeholder List View data with live garage data.
Performed integration testing between frontend and backend.
Resolved Visual Studio cache-related issues involving .vs, bin and obj.
Performed testing of:
Parking
Retrieve
Search
Move
Save
Load
List View
Garage Map updates
Working on:
Final testing and bug fixing.
Next step:
Resolve the JSON serialization issue related to save operations.
Perform final project review and submit the assignment.
Summary

Throughout Prague Parking 2.0, my primary responsibility was the development of the user interface and the integration between frontend and backend functionality.
While other team members focused largely on backend architecture, testing frameworks, file handling and data models,
I concentrated on building the Spectre.Console-based interface and connecting user interactions to the underlying system.

I designed and implemented the menu system, navigation flow, logging interface, admin functionality and interactive workflows.
During the final phase of the project, I connected the Parking, Retrieve, Search, Move, Save, Load and List functionalities to the backend implementation
while working closely with the code developed by the rest of the team.

One of the most valuable experiences during the project was learning how to work effectively with Git in a collaborative environment.
Frequent use of branches, commits, pull requests and merges helped reduce conflicts and made development easier to manage under time pressure.

The project also strengthened my understanding of integration work. Connecting independently developed frontend and backend components required reading existing code, 
understanding tests, troubleshooting issues and gradually building complete user workflows.
This process gave me practical experience of working in a larger shared codebase and increased my confidence in both C# development and collaborative software projects.
