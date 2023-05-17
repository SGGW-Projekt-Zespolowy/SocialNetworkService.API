﻿using Domain.Primitives;

namespace Domain.Entities
{
    public sealed class Specialization : Entity
    {
        public Specialization(Guid id, MedicalSpecializationEnum specialization, Guid authorId) : base(id)
        {
            MedicalSpecialization = specialization;
            AuthorId = authorId;
        }

        public MedicalSpecializationEnum MedicalSpecialization { get; set; }
        public Guid AuthorId { get; set; }
    }
}
