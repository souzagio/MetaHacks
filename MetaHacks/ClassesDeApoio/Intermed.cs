using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using System.Runtime.InteropServices;
using System.Net;
using System.Net.Mail;

namespace MetaHacks
{
    class Intermed
    {
        MySqlConnection Conexao = new MySqlConnection("server=127.0.0.1; uid=root; pwr=; port=3306; database=giobd;");
        public Int16 ID, iValidador = 0;
        public string varNome, varEmail, varRecSenha, varRecLogin;
        public DateTime dData, dAlter, dValidade;
        public byte[] varImagem = null;

        MySqlDataReader Dr;
        public string NeoCad(string login, string senha, string nome, string email)
        {
            string msg = string.Empty;
            try
            {
                Conexao.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "inset into Users values (NULL, @plogin, @psenha, @pnome, '2022-05-28 13:48:49', '2022-05-28 13:48:49', @pemail, '2022-05-30 13:48:49', NULL, NULL, 1)";
                cmd.Connection = Conexao;

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@plogin", login);
                cmd.Parameters.AddWithValue("@psenha", senha);
                cmd.Parameters.AddWithValue("@pnome", nome);
                cmd.Parameters.AddWithValue("@plogin", email);

                cmd.ExecuteNonQuery();
                Conexao.Close();
                msg = "Confirmado";
            }
            catch (MySqlException SqlErrr)
            {
                RetornaErroMySql retErro = new RetornaErroMySql();
                string msgErro = retErro.RetornaErro(SqlErrr.Number);
                if (string.IsNullOrEmpty(msgErro)) msgErro = SqlErrr.Message;
                msg = msgErro;
            }
            catch (Exception ex)
            {
                msg = ex.Message.ToString();
            }
            finally
            {
                if (Conexao.State == ConnectionState.Open) Conexao.Close();
            }
            return msg;
        }
        public string Cadastrar(string varLogin, string varNome, string varSenha, DateTime dData, string varEmail, Int32 iCodigo) //Done
        {
            string msg = string.Empty;
            try
            {
                Conexao.Open();
                MySqlCommand cmd = new MySqlCommand("STPCadastrar", Conexao);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = Conexao;

                cmd.Parameters.Clear();
                cmd.Parameters.Add("@pLogin", MySqlDbType.VarChar).Value = varLogin;
                cmd.Parameters.Add("@pSenha", MySqlDbType.VarChar).Value = varSenha;
                cmd.Parameters.Add("@pNome", MySqlDbType.VarChar).Value = varNome;
                cmd.Parameters.Add("@pData", MySqlDbType.Date).Value = dData;
                cmd.Parameters.Add("@pDtData", MySqlDbType.Date).Value = dData;
                cmd.Parameters.Add("@pEmail", MySqlDbType.VarChar).Value = varEmail;
                cmd.Parameters.Add("@pTempo", MySqlDbType.Date).Value = dData;
                cmd.Parameters.Add("@pCodigo", MySqlDbType.Int32).Value = iCodigo;


                cmd.ExecuteNonQuery();
                Conexao.Close();
                msg = "Confirmar";
            }
            catch (MySqlException SqlErrr)
            {
                RetornaErroMySql retErro = new RetornaErroMySql();
                string msgErro = retErro.RetornaErro(SqlErrr.Number);
                if (string.IsNullOrEmpty(msgErro)) msgErro = SqlErrr.Message;
                msg = msgErro;
            }
            catch (Exception ex)
            {
                msg = ex.Message.ToString();
            }
            finally
            {
                if (Conexao.State == ConnectionState.Open) Conexao.Close();
            }
            return msg;
        }
        public string AlterarUsuario(byte[] pPerfil, string pNome, string pSenha, DateTime pdData, string pEmail, Int16 iUse)
        {
            string msg = string.Empty;
            try
            {
                if (Conexao.State == ConnectionState.Open) Conexao.Close();
                Conexao.Open();
                MySqlCommand cmd = new MySqlCommand("STPAlterarRegistro", Conexao);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = Conexao;

                cmd.Parameters.Clear();
                cmd.Parameters.Add("@pUse", MySqlDbType.Int16).Value = iUse;
                cmd.Parameters.Add("@iPerfil", MySqlDbType.LongBlob).Value = pPerfil;
                cmd.Parameters.Add("@iSenha", MySqlDbType.VarChar).Value = pSenha;
                cmd.Parameters.Add("@iNome", MySqlDbType.VarChar).Value = pNome;
                cmd.Parameters.Add("@iData", MySqlDbType.Date).Value = pdData;
                cmd.Parameters.Add("@iEmail", MySqlDbType.VarChar).Value = pEmail;

                cmd.ExecuteNonQuery();
                Conexao.Close();
                msg = "Cadastro alterado com sucesso!!";
            }
            catch (MySqlException SqlErrr)
            {
                RetornaErroMySql retErro = new RetornaErroMySql();
                string msgErro = retErro.RetornaErro(SqlErrr.Number);
                if (string.IsNullOrEmpty(msgErro)) msgErro = SqlErrr.Message;
                msg = msgErro;
            }
            catch (Exception ex)
            {
                msg = ex.Message.ToString();
            }
            finally
            {
                if (Conexao.State == ConnectionState.Open) Conexao.Close();
            }
            return msg;
        }
        public string Logar(string varNome, string varSenha) //Done
        {
            string msg = string.Empty;

            Conexao.Open();
            MySqlCommand cmd = new MySqlCommand("STPLogin", Conexao);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = Conexao;

            cmd.Parameters.Clear();
            cmd.Parameters.Add("@pLogin", MySqlDbType.VarChar).Value = varNome;
            cmd.Parameters.Add("@pSenha", MySqlDbType.VarChar).Value = varSenha;

            MySqlParameter IdUser;
            IdUser = cmd.Parameters.Add("@oID", MySqlDbType.Int16);
            IdUser.Direction = ParameterDirection.Output;
            Dr = cmd.ExecuteReader();
            try
            {


                if (Dr.HasRows)
                {
                    Dr.Read();
                    ID = Convert.ToInt16(Dr["ID"].ToString());
                }
                Dr.Close();
                Conexao.Close();
            }
            catch (MySqlException SqlErrr)
            {
                RetornaErroMySql retErro = new RetornaErroMySql();
                string msgErro = retErro.RetornaErro(SqlErrr.Number);
                if (string.IsNullOrEmpty(msgErro)) msgErro = SqlErrr.Message;
                msg = msgErro;
            }
            catch (Exception Ex)
            {
                msg = Ex.Message.ToString();
            }
            finally { if (Conexao.State == ConnectionState.Open) Conexao.Close(); }
            return msg;
        }
        public string Conectar(Int16 inID) //Done
        {
            string msg = string.Empty;

            Conexao.Open();
            MySqlCommand cmd = new MySqlCommand("STPConectar", Conexao);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = Conexao;
            //Parâmetros de Entrada
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@pID", MySqlDbType.Int16).Value = inID;
            //Parâmetros de saída
            MySqlParameter pPerfil, pNome, pData, pEmail, pAlter, pTempo, pValidador;
            pNome = cmd.Parameters.Add("@oNome", MySqlDbType.VarChar);
            pNome.Direction = ParameterDirection.Output;
            pData = cmd.Parameters.Add("@oData", MySqlDbType.DateTime);
            pData.Direction = ParameterDirection.Output;
            pAlter = cmd.Parameters.Add("@oDtData", MySqlDbType.DateTime);
            pAlter.Direction = ParameterDirection.Output;
            pEmail = cmd.Parameters.Add("@oEmail", MySqlDbType.VarChar);
            pEmail.Direction = ParameterDirection.Output;
            pTempo = cmd.Parameters.Add("@oTempo", MySqlDbType.DateTime);
            pTempo.Direction = ParameterDirection.Output;
            pValidador = cmd.Parameters.Add("@oValidador", MySqlDbType.Int16);// A V E R I G U A R
            pValidador.Direction = ParameterDirection.Output;
            pPerfil = cmd.Parameters.Add("@oPerfil", MySqlDbType.LongBlob);
            pPerfil.Direction = ParameterDirection.Output;
            Dr = cmd.ExecuteReader();
            try
            {
                if (Dr.HasRows)
                {
                    Dr.Read();
                    varNome = Dr["Nome"].ToString();                    
                    dData = Convert.ToDateTime(Dr["Data"].ToString());
                    dAlter = Convert.ToDateTime(Dr["DtData"].ToString());
                    varEmail = Dr["Email"].ToString();
                    dValidade = Convert.ToDateTime(Dr["Tempo"]);
                    iValidador = Convert.ToInt16(Dr["Validador"].ToString());
                    varImagem = ((byte[])Dr["Perfil"]);
                    Dr.Close();
                    Conexao.Close();
                }
            }
            catch (MySqlException SqlErrr)
            {
                RetornaErroMySql retErro = new RetornaErroMySql();
                string msgErro = retErro.RetornaErro(SqlErrr.Number);
                if (string.IsNullOrEmpty(msgErro)) msgErro = SqlErrr.Message;
                msg = msgErro;
            }
            catch (Exception Ex)
            {
                msg = Ex.Message.ToString();
            }
            finally { if (Conexao.State == ConnectionState.Open) Conexao.Close(); }
            return msg;
        }
        public string Excluir(Int16 pID)
        {
            string msg = string.Empty;
            try
            {
                Conexao.Open();
                MySqlCommand cmd = new MySqlCommand("STPExcluir", Conexao);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = Conexao;

                cmd.Parameters.Clear();
                cmd.Parameters.Add("@pID", MySqlDbType.VarChar).Value = pID;
                cmd.ExecuteNonQuery();
                msg = "Usuário Excluído";
                Conexao.Close();
            }
            catch (MySqlException SqlErrr)
            {
                RetornaErroMySql retErro = new RetornaErroMySql();
                string msgErro = retErro.RetornaErro(SqlErrr.Number);
                if (string.IsNullOrEmpty(msgErro)) msgErro = SqlErrr.Message;
                msg = msgErro;
            }
            catch (Exception Ex)
            {
                msg = Ex.Message.ToString();
            }
            finally { if (Conexao.State == ConnectionState.Open) Conexao.Close(); }
            return msg;
        }
        public string Validar(Int16 valID, DateTime valDia, Int16 valOp)
        {
            string msg = string.Empty;
            // Adicionar 10 minutos na data do dia;
            if (valOp == 1)
            {
                try
                {
                    double s = 10;
                    DateTime dNew = valDia.AddMinutes(s);
                    Conexao.Open();
                    MySqlCommand cmd = new MySqlCommand("STPValidarTempo", Conexao);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = Conexao;

                    cmd.Parameters.Clear();
                    cmd.Parameters.Add("@pTempo", MySqlDbType.DateTime).Value = dNew;
                    cmd.Parameters.Add("@pID", MySqlDbType.Int16).Value = valID;
                    cmd.ExecuteNonQuery();
                    Conexao.Close();
                    dValidade = dNew;
                    msg = "Pin Validado com sucesso!!\nSeja bem vindo";
                }
                catch (MySqlException SqlErrr)
                {
                    RetornaErroMySql retErro = new RetornaErroMySql();
                    string msgErro = retErro.RetornaErro(SqlErrr.Number);
                    if (string.IsNullOrEmpty(msgErro)) msgErro = SqlErrr.Message;
                    msg = msgErro;
                }
                catch (Exception ex)
                {
                    msg = ex.Message.ToString();
                }
                finally
                {
                    if (Conexao.State == ConnectionState.Open) Conexao.Close();
                }

            }
            //Adicionar 31 dias da data atual
            else if (valOp == 2)
            {
                try
                {

                    double s = 31;
                    Conexao.Open();
                    MySqlCommand cmd;
                    MySqlDataReader Dr;

                    cmd = new MySqlCommand("Select tempo from users where Id = ?");
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add("@Id", MySqlDbType.VarChar).Value = valID;
                    cmd.Connection = Conexao;
                    cmd.CommandType = CommandType.Text;

                    Dr = cmd.ExecuteReader();
                    if (Dr.HasRows)
                    {
                        DateTime i;
                        Dr.Read();
                        i = DateTime.Parse(Dr["Tempo"].ToString());
                        Dr.Close();
                        if (i != null)
                        {
                            DateTime dNew = i.AddDays(s);
                            cmd = new MySqlCommand("STPValidarTempo", Conexao);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Connection = Conexao;

                            cmd.Parameters.Clear();
                            cmd.Parameters.Add("@pTempo", MySqlDbType.DateTime).Value = dNew;
                            cmd.Parameters.Add("@pID", MySqlDbType.Int16).Value = valID;
                            cmd.ExecuteNonQuery();
                            Dr.Close();
                            Conexao.Close();
                            dValidade = dNew;
                            msg = "Pin Validado com sucesso!!\n31D Adicionados à sua conta";
                        }
                        else
                        {
                            DateTime dNew = valDia.AddDays(s);
                            cmd = new MySqlCommand("STPValidarTempo", Conexao);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Connection = Conexao;

                            cmd.Parameters.Clear();
                            cmd.Parameters.Add("@pTempo", MySqlDbType.DateTime).Value = dNew;
                            cmd.Parameters.Add("@pID", MySqlDbType.Int16).Value = valID;
                            cmd.ExecuteNonQuery();
                            Conexao.Close();
                            Dr.Close();
                            dValidade = dNew;
                            msg = "Pin Validado com sucesso!!\n31D Adicionados à sua conta";
                        }

                    }
                    else
                    {
                        DateTime dNew = valDia.AddDays(s);
                        cmd = new MySqlCommand("STPValidarTempo", Conexao);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = Conexao;

                        cmd.Parameters.Clear();
                        cmd.Parameters.Add("@pTempo", MySqlDbType.DateTime).Value = dNew;
                        cmd.Parameters.Add("@pID", MySqlDbType.Int16).Value = valID;
                        cmd.ExecuteNonQuery();
                        Conexao.Close();
                        Dr.Close();
                        dValidade = dNew;
                        msg = "Pin Validado com sucesso!!\n31D Adicionados à sua conta";
                    }
                    //---------

                }
                catch (MySqlException SqlErrr)
                {
                    RetornaErroMySql retErro = new RetornaErroMySql();
                    string msgErro = retErro.RetornaErro(SqlErrr.Number);
                    if (string.IsNullOrEmpty(msgErro)) msgErro = SqlErrr.Message;
                    msg = msgErro;
                }
                catch (Exception ex)
                {
                    msg = ex.Message.ToString();
                }
                finally
                {
                    if (Conexao.State == ConnectionState.Open) Conexao.Close();
                }

            }
            return msg;
        } //Done
        public string RecSenha(string varEmail)
        {
            string msg = string.Empty;

            Conexao.Open();
            MySqlCommand cmd = new MySqlCommand("STPRecSenha", Conexao);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = Conexao;

            cmd.Parameters.Clear();
            cmd.Parameters.Add("@pEmail", MySqlDbType.VarChar).Value = varEmail;
            MySqlParameter pSenha, pLogin;
            pSenha = cmd.Parameters.Add("@oSenha", MySqlDbType.VarChar);
            pSenha.Direction = ParameterDirection.Output;
            pLogin = cmd.Parameters.Add("@oLogin", MySqlDbType.VarChar);
            pLogin.Direction = ParameterDirection.Output;
            Dr = cmd.ExecuteReader();
            try
            {
                if (Dr.HasRows)
                {
                    Dr.Read();
                    varRecSenha = Dr["Senha"].ToString();
                    varRecLogin = Dr["Login"].ToString();
                    msg = "Senha Recuperada!";
                }
                Conexao.Close();
                Dr.Close();
            }
            catch (MySqlException SqlErrr)
            {
                RetornaErroMySql retErro = new RetornaErroMySql();
                string msgErro = retErro.RetornaErro(SqlErrr.Number);
                if (string.IsNullOrEmpty(msgErro)) msgErro = SqlErrr.Message;
                msg = msgErro;
            }
            catch (Exception ex)
            {
                msg = ex.Message.ToString();
            }
            finally
            {
                if (Conexao.State == ConnectionState.Open) Conexao.Close();
            }
            return msg;
        }
        public string EnviarEmail(string varLog, string strDestinatario, string varValidar)
        {
            string msg = string.Empty;
            try
            {

                string strEmail = "Este é um e-mail automático, não responda este e-mail : \nOlá, " + varLog + ", para validar sua conta, utilize este código : "
                    + varValidar +".\nSe você não fez um cadastro em Meta Hacks - Giovany, por favor, ignore este e-mail.";
                MailMessage mensagem = new MailMessage("metahacksgiovany@gmail.com", strDestinatario);
                mensagem.Subject = "Validação de e-mail";
                mensagem.Body = strEmail;
                mensagem.SubjectEncoding = Encoding.GetEncoding("UTF-8");
                mensagem.BodyEncoding = Encoding.GetEncoding("UTF-8");

                SmtpClient smClient = new SmtpClient("smtp.gmail.com", 587);
                smClient.UseDefaultCredentials = false;
                smClient.Credentials = new NetworkCredential("metahacksgiovany@gmail.com", "Kg989365#");
                smClient.EnableSsl = true;
                smClient.Send(mensagem);
                msg = "E-mail enviado para sua caixa de entrada.\nVerifique-a para validar sua conta.";
            }
            catch
            {
                msg = "Não foi possível enviar o e-mail.";
            }
            return msg;
        }
        public string ConferirLogin(string varTestarLog) //Done
        {
            string msg = string.Empty;
            Conexao.Open();
            MySqlCommand cmd = new MySqlCommand("STPConferirLogin", Conexao);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = Conexao;

            cmd.Parameters.Clear();
            cmd.Parameters.Add("@pLogin", MySqlDbType.VarChar).Value = varTestarLog;

            MySqlParameter pLogin;
            pLogin = cmd.Parameters.Add("@oLogin", MySqlDbType.VarChar);
            pLogin.Direction = ParameterDirection.Output;
            Dr = cmd.ExecuteReader();
            try
            {

                if (Dr.HasRows)
                {
                    Dr.Read();
                    msg = Dr["Login"].ToString();
                }
                Dr.Close();
                Conexao.Close();
            }
            catch { }
            finally { if (Conexao.State == ConnectionState.Open) Conexao.Close(); }
            return msg;
        }
        
    }
    public class Erro
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
    }
    public class RetornaErroMySql
    {
        List<Erro> lstErros = new List<Erro>()
        {
            new Erro(){Id=1062, Descricao="Login ou e-mail já em uso,\nescolha um diferente"}
        };
        public string RetornaErro(int errorNumber)
        {
            string msgErro = string.Empty;
            Erro errInfo = new Erro();
            errInfo = lstErros.Find(p => p.Id == errorNumber);
            if (errInfo != null)
                msgErro = errInfo.Descricao;
            return msgErro;
        }
    }
}

