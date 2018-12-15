﻿using MineColony.Game.Systems;
using MineColony.Tests.Builders;
using MineColony.Tests.Utilities;
using NUnit.Framework;
using UnityEngine;

namespace MineColony.Tests.Systems
{
    public class TaskBacklog_Enqueue
    {
        [Test]
        public void TaskIsAddedToCollection()
        {
            TaskBacklog taskBacklog = new TaskBacklogBuilder().AddTaskCollection().AddOnTaskEnqueue().Build();

            Task task = new TaskBuilder().Build();
            taskBacklog.Enqueue(task);

            Assert.AreEqual(1, taskBacklog.TaskCollection.Count);
        }

        [Test]
        public void TaskIsAddedToTheEndOfTheCollection()
        {
            Task existingTask1 = new TaskBuilder().AddName("ExistingTask1").Build();
            Task existingTask2 = new TaskBuilder().AddName("ExistingTask1").Build();
            TaskBacklog taskBacklog = new TaskBacklogBuilder().AddTaskCollection(existingTask1, existingTask2).Build();

            Task task = new TaskBuilder().AddName("TheLastTask").Build();
            taskBacklog.Enqueue(task);

            Task lastTaskInBacklog = taskBacklog.TaskCollection[taskBacklog.TaskCollection.Count - 1];
            Assert.AreEqual(3, taskBacklog.TaskCollection.Count);
            Assert.AreEqual("TheLastTask", lastTaskInBacklog.name);
        }

        [Test]
        public void TaskIsAddedToCollectionWhenNoTriggerIsAttached()
        {
            TaskBacklog taskBacklog = new TaskBacklogBuilder().AddTaskCollection().Build();

            Task task = new TaskBuilder().Build();
            taskBacklog.Enqueue(task);

            Assert.AreEqual(1, taskBacklog.TaskCollection.Count);
        }

        [Test]
        public void GameEventTriggerFiresOnEnqueue()
        {
            GameEventListenerFacade gameEventListenerFacade = new GameEventListenerFacade();
            TaskBacklog taskBacklog = new TaskBacklogBuilder().AddTaskCollection().AddOnTaskEnqueue(gameEventListenerFacade).Build();

            Task task = new TaskBuilder().Build();
            taskBacklog.Enqueue(task);

            Assert.IsTrue(gameEventListenerFacade.EventWasRaised);
        }
    }
}