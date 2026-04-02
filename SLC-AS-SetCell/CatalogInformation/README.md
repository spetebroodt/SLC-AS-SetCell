# Set Cell

## About

This script sets a cell value in a table parameter of a DataMiner element. It is designed to be used with GQI (Generic Query Interface) or as a standalone automation script.

## Input Parameters

The script requires the following parameters:

| Name | Description | Format |
|------|-------------|--------|
| Element Identifier | The element containing the table | - Element name (e.g., `MyElement`)<br>- DataMinerID/ElementID (e.g., `123/456`). |
| Column Parameter Identifier | The column parameter ID | A valid integer representing the column parameter ID in the element's protocol (e.g., `1002`). |
| Primary Key | The primary key of the row | The unique identifier (string) of the table row where the cell value should be set. |
| Value | The value to set | N/A |

These parameters can also be filled in via a GQI query, allowing for dynamic input based on query results.

## Error Handling

The script can return the following error messages:

- The element identifier is empty or invalid
- The column parameter ID is empty, whitespace, or not a valid integer
- The primary key is empty or invalid
- The element cannot be found in the DataMiner System
- The element is not active
- The user does not have permission to set the cell
- The script encounters an error while attempting to set the cell
- An unexpected exception occurs during execution
