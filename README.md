# CalculatorApplication

I created a console application to built calculator. So after downloading the project from github open the solution in
Visual studio. Then click on run button to start the application.


The application takes two arbitrary expressions as arguments. It can be either integers or decimal numbers. 

My assumptions: 

Assumption 1:
The output will be in Integers or Decimals based on given input except for Division.
For division the output is always going to be an integer. The reason is, for handling decimal numbers we need to have float type. 
Consider a scenario :div(10,0). if the operands are floating type it returns infinity so to avoid this I converted the operands to integers during divison operation.
So for divison the result is always an integer

Assumption 2:
For subtraction : sub(2,3) or sub(3,2) always gives 1 as output.


NUnit Testing: I used nUnit testing framework for writing unit test cases.

Sample test cases:

Testcase 1:

Input : empty input

Output : Input is empty

Testcase 2:

Input : add(1,2))

Output : Incorrect braces. Enter the input in the correct format.

Testcase 3:

Input : subbbb(1,2)

Output : Wrong function names. Please enter function names as add(),sub(),mult(),div().

Testcase 4:

Input : mult(1,2,20)

Output : Invalid input format.

Testcase 5:

Input : add(12abc3,43ce)

Output : Input not in correct format. There should not be letters between numbers.
     
Testcase 6:

Input : add(sub(div(10,2),mult(2,3)),mult(add(20,30),sub(30,20)))

Output : 501.
    
   

