int testIf(int a, int b) {
	if (a == b) {
		return -1;
	}
	else
	{
		if (a > b) {
			return a;
		}
		else {
			return b;
		}
	}
}

int testWhile(int a) {
	int i;i = 0;
	while (i < a) {
		i = i + 1;
	}

	return i;
}

int testFor(int a) {
	int i;
	int b;
	b = 0;

	for (i = 0; i < a; i = i + 1) {
		b = b + 1;
	}

	return b;
}

int testRecurcif(int i) {
	if (i == 0) {
		return 0;
	}
	else if (i <= 2) {
		return 1;
	}

	return testRecurcif(i - 1) + testRecurcif(i - 2);

}

int testBreak()
{
	int a; a = 4;
	while(a < 10)
	{
		a = a + 1;
		if(a == 6)
		{
			break;
		}
	}

	if(a == 6)
	{
		return 1;
	}

	return 0;
}

int testContinue()
{
	int a; a = 4;
	int b;b = 0;
	while (a < 10)
	{
		a = a + 1;
		
		if(b == 0)
		{
			continue;
		}

		b = 1;
	}

	if (b == 0)
	{
		return 1;
	}

	return 0;
}

int main() {
	print(testIf(1,1)); // -1
	print(testIf(2,1)); // 2
	print(testIf(1,2)); // 2

	print(testWhile(10)); // 10
	
	print(testFor(10)); // 10
	
	print(testRecurcif(15)); // 610
	
	print(testBreak()); // 1
	print(testContinue()); // 1

}