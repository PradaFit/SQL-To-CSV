# SQL Converter Tool - Made By PradaFit (me)

**Welcome to the SQL Converter Tool Yo!**

This tool is designed to easily and efficiently convert SQL files into CSV format with a simple, user-friendly interface. Whether you're working with MySQL, PostgreSQL, or any other SQL-based database, our app focuses on converting `INSERT INTO` statements into a clean, (CSV) format.

## Features ✨

- **Simple UI:** Select your `.sql` file and convert it to `.csv` with just two clicks.
- **Progress Tracking:** See the conversion progress through the real-time progress bar.
- **Supports Large Files:** Handles large `.sql` files with ease using efficient streaming and async processing.
- **Custom CSV Format:** The output is in a clean, comma-separated format with **no spaces** like `test,here,mic check,yes`.
- **Cross-Database Compatibility:** Works with SQL statements from databases such as MySQL, PostgreSQL, and more.
- **Focused Conversion:** Converts only `INSERT INTO` SQL statements and skips everything else, ensuring fast and accurate conversion.

## How to Use the SQL Converter Tool

### Step-by-Step Instructions:

1. **Clone the repository** to your local machine:
    ```bash
    git clone https://github.com/PradaFit/SQL-To-CSV.git
    ```

2. **Open the project**

3. **Build the project**

4. **Run the application** by pressing `F5` or selecting `Debug > Start Debugging`.

5. Once the application starts, follow these steps:
    1. **Insert SQL File:** Click the `Insert` button to select the `.sql` file you want to convert.
    2. **Start Conversion:** Click the `Convert` button to convert your `.sql` file into `.csv` format.
    3. **Progress Tracking:** Watch the progress bar to monitor the conversion in real-time.
    4. **Conversion Complete!** Once the process finishes, you will see a confirmation message and your new `.csv` file will be saved in the same directory as the original `.sql` file.

## Reqs.

- **Windows OS** (Tested on Windows 8 through 11)
- **.NET Framework** (Ensure you have the latest version installed)
- **Compatible C# IDE**

## Example of Output 

Here’s what you can expect after conversion:

### CSV Output:
```csv
1,Alice,30
2,Bob,25
3,Charlie,35
```
