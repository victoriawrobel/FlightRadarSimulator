# FlightRadar - Flight Data Management Application

## Overview
FlightRadar is a flight data management application I developed using C# on one of the subjects during my Computer Science studies at the university for my BSc degree. This project emphasizes the application of Object-Oriented Design (OOD) principles to create a scalable, maintainable, and efficient software solution. It loads flight data from files, simulates real-time updates via a network source, synchronizes with a graphical user interface (GUI), and supports data manipulation through commands. Additionally, it generates news reports about flights and manages dynamic updates efficiently.

## Features
1. **Data Loading and Serialization**  
   The application loads flight data from a custom FTR file format, processes it into objects, and stores them in memory. The data can be serialized and saved in JSON format, ensuring the program is easily extendable for new data formats.

2. **Simulated Real-Time Data Updates**  
   Flight data is updated dynamically using a simulated network source, which broadcasts binary flight information. Commands such as `print` allow users to save snapshots of the current flight data, and the `exit` command gracefully shuts down the application.

3. **GUI Synchronization**  
   A graphical interface displays flight positions on a world map, with real-time updates on airplane movement. The GUI features zooming capabilities for closer inspection of airplane trajectories.

4. **News Generation**  
   The application includes a system to generate news about flights and airports, with support for multiple news providers like TV, radio, and newspapers. Reports are generated using a `NewsGenerator` class and displayed through a custom `report` command.

5. **Event-Driven Updates**  
   The system supports real-time updates to flight data through events (such as ID updates, position changes, and contact information updates) and logs changes to a file. Logs are managed by day, ensuring clear records of modifications.

6. **Command System for Data Queries**  
   A flexible command system allows users to query, update, delete, and add flight-related data. Commands include:
   - `display` for viewing data
   - `update` for modifying data
   - `delete` for removing entries
   - `add` for introducing new data
   All commands support conditions and key-value pairs for precise data manipulation.

## Technologies and Design Principles
- **Object-Oriented Design (OOD)**  
  The entire system is designed with OOD principles, ensuring modular, scalable, and maintainable code. Components are well-structured to allow for easy expansion, such as adding new data sources or reportable objects.
  
- **Serialization and Deserialization**  
  Data is serialized to JSON for storage and easily deserialized for processing, making the system adaptable to future data formats.

- **GUI Integration**  
  A GUI component was implemented using a provided package to visualize airplane data on a real-time world map.

- **Command Parsing and Execution**  
  The command system is designed to allow flexible and efficient manipulation of data through queries and conditions.

## Usage
The application is designed to be run via the command line with commands like:
- `display` to show flight data in a table
- `update` to modify specific attributes of an object
- `delete` to remove objects based on conditions
- `add` to introduce new flights or related data

The system logs updates dynamically and allows snapshots of the current state to be saved as JSON files.
