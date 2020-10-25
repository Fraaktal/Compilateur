

int testAddition()
{
	int a;int b;
	a = 3;
	b = 1;
	print(a + b); // 4

	a = -a;
	print(a + b); // -2

	b = -b;
	print(a + b); // -4
	
}

int testSoustraction()
{
	int a;
	a = 1 - 2;
	print(a); 
}

int testMultiplication()
{

}

int testDivision()
{

}


int main() {
	testPrint();
	testAddition();
	testSoustraction();
	testMultiplication();
	testDivision();
}