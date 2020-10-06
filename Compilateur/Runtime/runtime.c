int init() {
	return 0;
}

int pow(int a, int b){
	int r;
	r = 1;
	while(b!=0){
		r = r * a;
		b = b - 1;
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


int print(int n)
{
	if(n<0)
	{
		send(45);
		n = -n;
	}
	if(n == 0)
	{
		send(48);
	}
	else
	{
		print_sub(n);
	}
}

int print_ref(int n)
{
	if(n == 0)
	{
		return 0;
	}
	int r;
	int d;
	r = n / 10;
	d = n % 10;
	print_ref(r);
	send(d + 48);
}




