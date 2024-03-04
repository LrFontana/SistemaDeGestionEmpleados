namespace Client.EstadosDeLaAplicacion
{
    public class TodosLosEstados
    {
        // Propiedades.
        public Action? Accion {  get; set; }
        public bool MostrarDepartamentoGeneral { get; set; }
        public bool MostrarDepartamento { get; set; }
        public bool MostrarRama { get; set; }
        public bool MostrarPais { get; set; }
        public bool MostrarCiudad { get; set; }
        public bool MostrarPueblo { get; set; }
        public bool MostrarEmpleado { get; set; }
        public bool MostrarUsuario{ get; set; }

        // Metodos.

        // Click departamento general.
        public void CickDepartamentoGeneral()
        {
            ReiniciarTodosLosDepartamentos();
            MostrarDepartamentoGeneral = true;
            Accion?.Invoke();
        }

        // Mostrar Departamento Click.
        public void ClickMostrarDepartamento()
        {
            ReiniciarTodosLosDepartamentos();
            MostrarDepartamento = true;
            Accion?.Invoke();
        }

        // Mostrar Rama Click.
        public void ClickMostrarRama()
        {
            ReiniciarTodosLosDepartamentos();
            MostrarRama = true;
            Accion?.Invoke();
        }

        // Mostrar Pais Click.
        public void ClickMostrarPais()
        {
            ReiniciarTodosLosDepartamentos();
            MostrarPais = true;
            Accion?.Invoke();
        }

        // Mostrar Ciudad Click.
        public void ClickMostrarCiudad()
        {
            ReiniciarTodosLosDepartamentos();
            MostrarCiudad = true;
            Accion?.Invoke();
        }

        // Mostrar Pueblo Click.
        public void ClickMostrarPueblo()
        {
            ReiniciarTodosLosDepartamentos();
            MostrarPueblo = true;
            Accion?.Invoke();
        }

        // Mostrar Empleado Click.
        public void ClickMostrarEmpleado()
        {
            ReiniciarTodosLosDepartamentos();
            MostrarEmpleado = true;
            Accion?.Invoke();
        }

        // Mostrar Usuario Click.
        public void ClickMostrarUsuario()
        {
            ReiniciarTodosLosDepartamentos();
            MostrarUsuario = true;
            Accion?.Invoke();
        }

        // Reiniciar  todos los departamentos.
        private void ReiniciarTodosLosDepartamentos()
        {
            MostrarDepartamentoGeneral = false;
            MostrarDepartamento = false;
            MostrarRama = false;
            MostrarPais = false;
            MostrarCiudad = false;
            MostrarPueblo = false;
            MostrarEmpleado = false;
            MostrarUsuario = false;

        }
    }
}
