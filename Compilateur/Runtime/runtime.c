int init() {
	return 0;
}

int puissance(int a, int b){
	int r;
	r = 1;
	int neg; neg = 0;
	if (b < 0) {
		b = -b;
		neg = 1;
	}
	while(b!=0){
		r = r * a;
		b = b - 1;
	}
	if (neg == 1) {
		r = 1 / r;
	}
	return r;
}

int free(int p){
	return 0;
}

int malloc(int n){
	int p;
	p = *0;
	*0 = *0 + n;
	return p;
}

int printSub(int n)
{
	int r;
	int d;
	r = n / 10;
	d = n % 10;
	if (r != 0) 
	{
		printSub(r);
	}
	send d + 48;
}

int print(int n)
{
	if(n<0)
	{
		send 45;
		n = -n;
	}
	if(n == 0)
	{
		send 48;
	}
	else
	{
		printSub(n);
	}
	
	send 10;
}

int readline()
{
	int a;
	int result;
	result = 0;
	a = receive;

	while (a != 10) {
		result = (result * 10) + a - 48;
		a = receive;
	}

	return result;
}