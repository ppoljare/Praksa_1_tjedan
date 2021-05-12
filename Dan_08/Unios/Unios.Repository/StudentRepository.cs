﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using Unios.Model;
using Unios.Model.Common;
using Unios.Repository.Common;
using Unios.Repository.Entities;
using System.Threading.Tasks;

namespace Unios.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private static readonly string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        private readonly SqlConnection Connection = new SqlConnection(_connectionString);
        

        public async Task<IStudent> AddAsync(IStudent student)
        {
            var config = new MapperConfiguration(cfg =>
                cfg.CreateMap<IStudent, StudentEntity>()
            );
            var mapper = new Mapper(config);
            var studentEntity = mapper.Map<StudentEntity>(student);

            string nonQueryString =
                "INSERT INTO Student VALUES ('" +
                studentEntity.StudentID + "', '" +
                studentEntity.FakultetID + "', '" +
                studentEntity.Ime + "', '" +
                studentEntity.Prezime + "', " +
                studentEntity.Godina + ")";

            SqlCommand comm = new SqlCommand(nonQueryString, Connection);
            await Connection.OpenAsync();
            try
            {
                await comm.ExecuteNonQueryAsync();
            }
            catch (Exception)
            {
                Connection.Close();
                return null;
            }

            Connection.Close();

            student.Fakultet = await FindFakultet(student.FakultetID);
            return student;
        }


        public async Task<int> DeleteAsync(Guid id)
        {
            string nonQueryString =
                "DELETE FROM Student " +
                "WHERE StudentID = '" + id + "'";

            SqlCommand comm = new SqlCommand(nonQueryString, Connection);
            await Connection.OpenAsync();
            try
            {
                await comm.ExecuteNonQueryAsync();
            }
            catch (Exception)
            {
                Connection.Close();
                return -400;
            }

            Connection.Close();

            return 0;
        }


        public async Task<List<IStudent>> FindAsync()
        {
            List<IStudent> storage = new List<IStudent>();

            string queryString =
                "SELECT StudentID, Ime, Prezime, Naziv, Godina " +
                "FROM Student JOIN Fakultet " +
                "ON (Student.FakultetID = Fakultet.FakultetID)";

            SqlCommand comm = new SqlCommand(queryString, Connection);

            await Connection.OpenAsync();
            SqlDataReader dataReader = await comm.ExecuteReaderAsync();

            while (dataReader.Read())
            {
                IStudent student = new Student (
                    Guid.Parse(dataReader[0].ToString()),
                    dataReader[1].ToString(),
                    dataReader[2].ToString(),
                    dataReader[3].ToString(),
                    int.Parse(dataReader[4].ToString())
                );

                storage.Add(student);
            }

            Connection.Close();

            return storage;
        }


        public async Task<IStudent> GetAsync(Guid id)
        {
            IStudent student;

            string queryString =
                "SELECT StudentID, Ime, Prezime, Naziv, Godina " +
                "FROM Student JOIN Fakultet " +
                "ON (Student.FakultetID = Fakultet.FakultetID) " +
                "WHERE (StudentID = '" + id + "')";

            SqlCommand comm = new SqlCommand(queryString, Connection);

            await Connection.OpenAsync();
            SqlDataReader dataReader = await comm.ExecuteReaderAsync();

            if (dataReader.Read())
            {
                student = new Student(
                    Guid.Parse(dataReader[0].ToString()),
                    dataReader[1].ToString(),
                    dataReader[2].ToString(),
                    dataReader[3].ToString(),
                    int.Parse(dataReader[4].ToString())
                );

                Connection.Close();
                return student;
            }
            else
            {
                Connection.Close();
                return null;
            }
        }


        public async Task<IStudent> UpdateAsync(IStudent student)
        {
            var config = new MapperConfiguration(cfg =>
                cfg.CreateMap<IStudent, StudentEntity>()
            );
            var mapper = new Mapper(config);
            var studentEntity = mapper.Map<StudentEntity>(student);

            string nonQueryString =
                "UPDATE Student " +
                "SET FakultetID = '" + studentEntity.FakultetID + "', " +
                "Ime = '" + studentEntity.Ime + "', " +
                "Prezime = '" + studentEntity.Prezime + "', " +
                "Godina = " + studentEntity.Godina + " " +
                "WHERE StudentID = '" + studentEntity.StudentID + "'";

            SqlCommand comm = new SqlCommand(nonQueryString, Connection);
            await Connection.OpenAsync();
            try
            {
                await comm.ExecuteNonQueryAsync();
            }
            catch (Exception)
            {
                Connection.Close();
                return null;
            }

            Connection.Close();
            student.Found = true;
            student.Fakultet = await FindFakultet(student.FakultetID);
            return student;
        }


        private async Task<string> FindFakultet(Guid id)
        {
            string queryString =
                "SELECT Naziv " +
                "FROM Fakultet " +
                "WHERE FakultetID = '" + id + "'";

            SqlCommand comm = new SqlCommand(queryString, Connection);
            await Connection.OpenAsync();
            SqlDataReader dataReader = await comm.ExecuteReaderAsync();

            if (dataReader.Read())
            {
                string result = dataReader[0].ToString();
                Connection.Close();
                return result;
            }
            else
            {
                Connection.Close();
                return null;
            }
        }
    }
}
