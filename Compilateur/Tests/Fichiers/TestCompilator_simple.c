

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
	a = 3 - 2;
	print(a); // 1
	
	a = 2 - 3;
	print(a); // -1
	
	int b; int c;
	b = 3; c = 2;
	a = b - c;
	print(a); // 1

	a = c - b;
	print(a); // -1
}

int testMultiplication()
{

}

int testDivision()
{

}


int main() {
	testAddition();
	testSoustraction();
	testMultiplication();
	testDivision();
}