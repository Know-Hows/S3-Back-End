The Article model contains the variables that will contain the information of the Article. The Id for each individual Article is automatically made by MongoDB and assigned to the article.

Article contains:

    string?: Id
    string: EmployeeName
    string: Body
    int: LikesScore


When the Article model gets a new variable it's important to note that the variable can be returned null or not, this is done by adding a ? after the type of the variable.