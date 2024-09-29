
using System.Data;

namespace K_Mean
{

    struct Pixel
        {
            int r;
            int g;
            int b;
            public Pixel(int r, int g, int b)
            {
                this.r = r;
                this.g = g;
                this.b = b;
            }
            public Pixel(){
                r=0;
                b=0;
                g=0;
            }

            public readonly int GetRed(){
                return r;
            }
            public readonly int GetGreen(){
                return g;
            }
            public readonly int GetBlue(){
                return b;
            }

            public void SetRed(int r){
                this.r+=r;
            }
            public void SetGreen(int g){
                this.g+=g;
            }
            public void SetBlue(int b){
                this.b+=b;
            }
        }
    class KMean
    {
        private readonly Random random;

        public Pixel[] Centroîde{get;set;}
        public Pixel[] Pixels { get; set; }

        public KMean(){
            random=new();
        }


        public static double Distance(Pixel p,Pixel p2){
            
            double distance=Math.Sqrt(Math.Pow(p.GetBlue() - p2.GetBlue(), 2.0) +Math.Pow(p.GetRed() - p2.GetRed(), 2.0) +Math.Pow(p.GetGreen() - p2.GetGreen(), 2.0));
            
            return distance;
        }

        
        public  double[][] Distances(List<List<Pixel>> image){
            
            
            int size=image.Count*image[0].Count;
            double[][] distances =new double[Centroîde.Length][];
            
            for (int i = 0; i < Centroîde.Length; i++)
            {
                int e=0;
                double[] d=new double[size];
                for (int j = 0; j < image.Count; j++)
                { 
                    for (int k = 0; k < image[j].Count; k++)
                    {
                        
                        d[e]=Distance(Centroîde[i],image[j][k]);
                        e++;
                    } 
                }
                distances[i]=d;
            }
            
            return distances;
        }

        private static double[] ExtractVector(double[][] X,int j){

            double[] array=new double[X.Length];
            for (int i = 0; i < X.Length; i++)
            {
                array[i]=X[i][j];
            }
            return array;
        }

        public void Fit(List<List<Pixel>> image,int K,int max_iter=10){
            
            Pixels=new Pixel[image.Count*image[0].Count];
            
            Centroîde=new Pixel[K];
            for (int i = 0; i < K; i++)
            {
                int t=random.Next()%image[0].Count;
                int z=random.Next()%image.Count;
                Centroîde[i]=image[t][z];
            }

            int k=0;
            while (k<max_iter)
            {
                double[][] distances =Distances(image);
                int[][] image_class= ImageClass(image,distances);
                UpdateCentroïde(image_class,image);
                k++;
            }
        }

        
        private void UpdateCentroïde(int[][] classes,List<List<Pixel>> image){

            int t=0;
            for (int i = 0; i < Centroîde.Length; i++)
            {
                List<Pixel> pixels=[];
                
                for (int j = 0; j < image.Count; j++)
                {

                  for (int k = 0; k < image[j].Count; k++)
                  {
                    
                    if (classes[j][k]==i)
                    {
                        pixels.Add(image[j][k]);
                        Pixels[t]=image[j][k];
                        t++;
                    }

                  }

                }
                
                Centroîde[i]=Mean(pixels);

            }
            
        }

        
        private static int[][] ImageClass(List<List<Pixel>> image,double[][] distances){
            
            
            int[][] classes=new int[image.Count][];
            
            int k=0;
            for (int i = 0; i < image.Count; i++)
            {
                
                classes[i]=new int[image[i].Count];
                for (int j = 0; j < image[i].Count; j++)
                {
                
                   classes[i][j]=GetClass(ExtractVector(distances,k));
                   k++;
                }
                
            }
            return classes;
        }

        private static Pixel Mean(List<Pixel> pixels){
            
            Pixel p=new();
            for (int i = 0; i < pixels.Count; i++)
            {
                p.SetRed(pixels[i].GetRed() / pixels.Count);
                p.SetGreen(pixels[i].GetGreen()/pixels.Count);
                p.SetBlue(pixels[i].GetBlue()/pixels.Count);
            }

            return p;
        }

        private static int GetClass(double[] distances){
            
            double min=distances[0];
            int index=0;
            for (int i = 1; i < distances.Length; i++)
            {
                if (distances[i]<min)
                {
                    min=distances[i];
                    index=i;
                }
            }
            
            return index;
        }
    }
}