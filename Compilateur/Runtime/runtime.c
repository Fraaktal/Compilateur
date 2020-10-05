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

