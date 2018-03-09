namespace Buzy.DataAccess.Model
{
    public class VeiculoAPI
    {
        public VeiculoAPI()
        {
        }


        public int p { get; set; } //Prefixo do veículo
        public bool a{ get; set; } //Indica se o veículo é (true) ou não (false) acessível para pessoas com deficiência 
        public string ta{ get; set; } //Indica o horário universal (UTC) em que a localização foi capturada. Essa informação está no padrão ISO 8601
        public double py { get; set; } //Informação de latitude da localização do veículo 
        public double px { get; set; } //Informação de longitude da localização do veículo
    }
}