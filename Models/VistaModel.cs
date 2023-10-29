namespace ProyectoFinalDesarrolloWeb.Models
{
    public class VistaModel
    {
        public int TotalItems { get; private set; }
        public int PaginaActual { get; private set; }
        public int TamanioPagina { get; private set; }
        public int TotalPaginas { get; private set; }
        public int PaginaInicial { get; private set; }
        public int PaginaFinal { get; private set; }

        public VistaModel()
        {

        }

        public VistaModel(int totalItems, int pagina, int tamanioPagina = 10)
        {
            int totalPaginas = (int)Math.Ceiling((decimal)totalItems / (decimal)tamanioPagina);
            int paginaActual = pagina;
            int paginaInicial = paginaActual - 5;
            int paginaFinal = paginaActual + 4;

            if (paginaInicial <= 0)
            {
                paginaFinal = paginaFinal - (paginaInicial - 1);
                paginaInicial = 1;
            }

            if (paginaFinal > totalPaginas)
            {
                paginaFinal = totalPaginas;
                if (paginaFinal > 10)
                {
                    paginaInicial = paginaFinal - 9;
                }
            }

            TotalItems = totalItems;
            PaginaActual = paginaActual;
            TamanioPagina = tamanioPagina;
            TotalPaginas = totalPaginas;
            PaginaInicial = paginaInicial;
            PaginaFinal = paginaFinal;
    }
    }
}
