// See https://aka.ms/new-console-template for more information
using K_Mean;
int n=100,m=100,k=2;
int iter_max = 500;


List<List<Pixel>> image=GetPixels(n,m);

KMean km=new();
km.Fit(image,k,iter_max);


Pixel[][] reschape=Reschape(km.Pixels,n,m);



System.Console.WriteLine();
foreach (var item in reschape)
{
    foreach (var p in item)
    {
        System.Console.Write($"[{p.GetRed()} {p.GetGreen()} {p.GetBlue()}]  ");
    }
    System.Console.WriteLine();
}
System.Console.WriteLine();


static List<List<Pixel>> GetPixels(int n,int m){
    List<List<Pixel>> pixels=[];
    Random random=new();
    for (int i = 0; i < n; i++){
        List<Pixel>p=[];
        for (int j = 0; j < m; j++)
        {
            p.Add(new Pixel(random.Next()%255,random.Next()%255,random.Next()%255));
        }
        pixels.Add(p);
    }
    return pixels;
}


static Pixel[][] Reschape(Pixel [] T,int n,int m){

    int k=0;

    Pixel[][] image=new Pixel[n][];

    while (k<T.Length)
    {
        for (int i = 0;i < n; i++)
        {
            
            image[i]=new Pixel[m];
            for (int j = 0;j < m; j++)
            {
                
                image[i][j]=T[k];
                k++;
            }
        }
    }
    return image;
}