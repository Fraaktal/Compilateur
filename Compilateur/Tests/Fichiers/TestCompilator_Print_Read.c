int testPrint()
{
	print(0); // 0
	print(1); // 1
	print(-1); // -1
	print(1 + 2); // 3
	print(1 - 2); // -1
	print(1 * 2); // 2
	print(1 / 2); // 0

	int a; int b;
	a = 1; b = 2;
	print(a); // 1
	print(-a); //-1
	print(a + b); // 3
	print(a - b); // -1
	print(a * b); // 2
	print(a / b); // 0
}

int main() {
	testPrint();
}