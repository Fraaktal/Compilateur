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
	
	print(1 + -1); // 0
	
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

	print(1 - 1); // 0
}

int testMultiplication()
{
	int a; a = 6;
	int b; b = 7;
	print(a * b); // 42
	print(-a * b); // -42
	print(-a * -b); // 42
	print(6 * 7); // 42
	int c;
	c = a * b;
	print(c); // 42
}

int testDivision()
{
	int a; a = 42;
	int b; b = 7;
	print(a / b); // 6
	print(-a / b); // -6
	print(-a / -b); // 6
	print(42 / 7); // 6
	int c;
	c = a / b;
	print(c); // 6
}

int testModulo()
{
	int a; a = 10;
	int b; b = 3;
	print(a % b); // 1
	print(-a % b); // -1
	print(-a % -b); // -1
	print(10 % 3); // 1
	int c;
	c = a % b;
	print(c); // 1
}


int main() {
	testAddition();
	testSoustraction();
	testMultiplication();
	testDivision();
	testModulo();
}