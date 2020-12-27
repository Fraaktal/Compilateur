int affectValue(int var, int val)
{
	*var = val;
}

int testPointer()
{
	int a; a = malloc(1);
	affectValue(a, 6);
	print(*a);
}


int testArray()
{
	int array;array = malloc(3);
	array[0] = 1;
	*(array + 1) = 2;
	array[2] = 3;
	print(array[0]);
	print(array[1]);
	print(*(array + 2));
}

int main() {
	testPointer();
	testArray();
}