Countries Data Collection and Management Tool

The Data Collection and Management Tool is a test application designed to fetch countries data from a public API, organizing it into short-description format, and providing filtering, pagination, and sorting.

Key Features:
    API Data Retrieval: The application integrates seamlessly with public REST Country API, allowing users to fetch data from external source.

    Filtering: The application offers filtering rules that enable users to extract only the data they need.

    Pagination: Users can specify the amount of data to retrieve in each request.

    Sorting: Allows arranging data in ascending or descending order.

How to run:
	Open Validation_Project.csproj in Visual Studio

	Choose IIS Express as the execution profile. Note: application can be run under the Kestrel, but it will require changing port option in the Client page.

	Run the application.

	Open .\Validation_Project\Client\index.html

Examples:
	Enter "ukr" in the Country name field and press "Submit". Result: in the Countries list must be only 1 country - Ukraine.

	Enter "ukr" in the Country name field and "10" in the Max population field and press "Submit" Result: Countries list must be empty since Ukrainse does not fit the criteria.

	Enter "10" in the Max population field and set "Order of sorting" to "Ascend" and press "Submit". Result: only countries with population less than 10m appeared.

	Enter "10" in the Max population field and set "Order of sorting" to "Descend" and press "Submit". Result: the same as in the previous step, but records are sorted by name descend.

	Enter "10" in the Max population field, set "Order of sorting" to "Ascend", set "Countries limit" to "10" and press "Submit". Result: only first 10 countries which met criteria appeared.

	Enter "10" in the Max population field, set "Order of sorting" to "Descend", set "Countries limit" to "10" and press "Submit". Result: the same as in the previous step, but records are sorted by name descend.

	Enter "5" in the Max population field, set "Order of sorting" to "Ascend", set "Countries limit" to "10" and press "Submit". Result: only first 10 countries which met criteria appeared, result is different from the previous two step since max population criteria has changed.

	Enter "g" in the Country name field, set "5" in the Max population field, set "Order of sorting" to "Ascend", set "Countries limit" to "10" and press "Submit". Result: only first 10 countries which met criteria appeared, result is different from the previous step since criteria has changed.

	Enter "uniTeD" in the Country name field and press "Submit". Result: even though the target name is written with incorrect cases matched countries are still appearing.

	Leave all fields empty and press "Submit". Result: all countries are displayed.