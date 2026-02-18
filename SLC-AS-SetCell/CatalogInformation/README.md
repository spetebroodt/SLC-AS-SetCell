# Set Cell

This script sets a cell value in a table parameter of a DataMiner element. It is designed to be used with GQI (Generic Query Interface) or as a standalone automation script.

## Input Parameters

The script requires the following parameters:

| Name | Description | Format |
|------|-------------|--------|
| Element Identifier | The element containing the table | Element name OR `DataMinerID/ElementID` (e.g., `123/456`) |
| Column Parameter Identifier | The column parameter ID | Integer value (e.g., `1002`) |
| Primary Key | The primary key of the row | String value |
| Value | The value to set | N/A |

### Parameter Details

- **Element Identifier**: Can be specified in two ways:
  - By element name (e.g., `MyElement`)
  - By DataMiner ID and Element ID (e.g., `123/456`)
  
- **Column Parameter Identifier**: Must be a valid integer representing the ID of the column parameter in the element's protocol.

- **Primary Key**: The unique identifier of the table row where the cell value should be set.

These parameters can also be filled in via feeds from a GQI query, allowing for dynamic input based on query results.

## Error Handling

The script will exit with an error message if:
- The element identifier is empty or invalid
- The column parameter ID is empty, whitespace, or not a valid integer
- The primary key is empty or invalid
- The element cannot be found in the DataMiner System
- The element is not active
- The user does not have permission to set the cell
- The script encounters an error while attempting to set the cell
- An unexpected exception occurs during execution