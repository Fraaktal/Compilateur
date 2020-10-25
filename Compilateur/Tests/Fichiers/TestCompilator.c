
int fibonacci(int i) {
	if (i == 0) {
		return 0;
	}
	else if (i <= 2) {
		return 1;
	}

	return fibonacci(i - 1) + fibonacci(i - 2);

}

int initTableau(int a, int taille) {
	int i;
	for (i = 0; i < taille; i = i + 1) {
		a[i] = i;
	}
}

int testWhile(int a) {
	int i;
	i = 0;
	while (i < a) {
		i = i + 1;
	}

	return i;
}

int testFor(int a) {
	int i;
	int b; 
	b = 0;

	for (i = 0; i < a ;i=i+1) {
		b = b + 1;
	}

	return b;
}

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

int main() {
	int a;
	a = fibonacci(15);
	print(a);

	a = testWhile(10);
	print(a);

	int b; b = testFor(20);
	print(b);

	int c; c = testIf(a, b);
	print(c);




	//Test tableau et pointeurs
	//int b; b = malloc(10);
	//initTableau(b, 10);
	//int i;
	//for (i = 0; i < 10; i = i + 1) {
	//	print(b[i]);
	//}
}