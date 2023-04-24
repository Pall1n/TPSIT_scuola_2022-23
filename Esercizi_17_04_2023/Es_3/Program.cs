/*
    Creare un Programma in c# che con due thread esegue un 
    countDown da 100 a 0 e un countUp da 0 a 100
*/

class Program {
    static void Main(string[] args) {
        Thread countDown = new Thread(new ThreadStart(CountDown));
        Thread countUp = new Thread(new ThreadStart(CountUp));

        countDown.Start();
        countUp.Start();

        countDown.Join();
        countUp.Join();
    }

    static void CountDown() {
        for (int i = 100; i >= 0; i--) {
            Console.WriteLine(i);
        }
    }

    static void CountUp() {
        for (int i = 0; i <= 100; i++) {
            Console.WriteLine(i);
        }
    }
}

/*
    Creo 2 thread che eseguono 2 metodi che fanno rispettivamente 
    un countDown e un countUp, stampando i numeri su console. 
    Si può notare come l'ordine dei numeri stampati su console 
    vari ad ogni esecuzione, poiché i 2 thread vengono eseguiti 
    in parallelo.
*/